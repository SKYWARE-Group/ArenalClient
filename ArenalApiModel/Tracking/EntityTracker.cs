using Skyware.Arenal.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml;

namespace Skyware.Arenal.Tracking;

/// <summary>
/// Change tracking for <see cref="EntityBase"/> objects.
/// </summary>
public static class EntityTracker
{

    private static string GetPath(string basePath, string value) => $"{(!string.IsNullOrEmpty(basePath) ? basePath + "." : string.Empty)}{value}";

    /// <summary>
    /// Observes changes in two objects (<see cref="EntityBase"/>)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="original"></param>
    /// <param name="target"></param>
    /// <param name="parentPath"></param>
    /// <returns></returns>
    public static IEnumerable<EntityChange> CompareTo<T>(this T original, T target, string parentPath = null)
    {


        if (typeof(IEnumerable).IsAssignableFrom(original.GetType()))
        {
            //if (target is null)
            //{
            //    //Original is Enumerable and target is null
            //    yield return new EntityChange()
            //    {
            //        PropertyName = original.GetType().Name,
            //        ChangeType = EntityChange.ChangeTypes.Changed,
            //        OldValue = original,
            //        NewValue = target
            //    };
            //    yield break;
            //}
            //else //both values are T! if (typeof(IEnumerable).IsAssignableFrom(target.GetType()))
            //{
            //    //Target is not null
            //    foreach (object x in (IEnumerable)original)
            //    {
            //        //nulls

            //    }
            //}
            if (original is IEnumerable<Service> services)
            {
                foreach (EntityChange change in services.CompareServicesTo((IEnumerable<Service>)target)) { yield return change; }
            }
            yield break;
        }
        else
        {
            //New is null
            if (target is null)
            {
                yield return new EntityChange()
                {
                    PropertyName = original.GetType().Name,
                    ChangeType = EntityChange.ChangeTypes.Changed,
                    OldValue = original,
                    NewValue = target
                };
                yield break;
            }

            PropertyInfo[] props = original.GetType().GetProperties();

            //Comparable types that are read/write
            foreach (PropertyInfo prop in props.Where(p => p.CanRead && p.CanWrite))
            {
                object oldPropVal = prop.GetValue(original);
                object newPropVal = target.GetType().GetProperty(prop.Name).GetValue(target);
                if ((oldPropVal is null && newPropVal is not null) ||
                    (oldPropVal is IComparable eqp && eqp.CompareTo(newPropVal) != 0))
                {
                    yield return new EntityChange()
                    {
                        PropertyName = GetPath(parentPath, prop.Name),
                        ChangeType = EntityChange.ChangeTypes.Changed,
                        OldValue = oldPropVal,
                        NewValue = newPropVal
                    };
                }
            }

            //Objects, defined in Skyware.Arenal.Model
            foreach (PropertyInfo prop in props.Where(p => original.GetType().GetProperty(p.Name).GetValue(original) is not null && original.GetType().GetProperty(p.Name).GetValue(original).GetType().FullName.StartsWith(original.GetType().Namespace)))
            {
                foreach (EntityChange c in original.GetType().GetProperty(prop.Name).GetValue(original).CompareTo(original.GetType().GetProperty(prop.Name).GetValue(target), GetPath(parentPath, prop.Name)))
                    yield return c;
            }
        }


    }

    /// <summary>
    /// Compares collections of <see cref="Identifier"/>
    /// </summary>
    /// <param name="original"></param>
    /// <param name="target"></param>
    /// <param name="parentPath"></param>
    /// <returns></returns>
    public static IEnumerable<EntityChange> CompareServicesTo(this IEnumerable<Service> original, IEnumerable<Service> target, string parentPath = null)
    {
        if (target is null)
        {
            int cnt = 0;
            foreach (Service service in original)
            {
                yield return new EntityChange()
                {
                    ChangeType = EntityChange.ChangeTypes.Deleted,
                    NewValue = null,
                    OldValue = service,
                    PropertyName = parentPath + $"[{cnt}]"
                };
                cnt++;
            }
            yield break;
        }

        IEnumerable<(Service, Service)> innerJoin = original.Join(target, (left) => left.ServiceId, (right) => right.ServiceId, (a, b) => (a, b));
        foreach ((Service, Service) svcPair in innerJoin)
        {
            foreach (EntityChange change in svcPair.Item1.CompareTo(svcPair.Item2)) { yield return change; }
        }
        
        var z = 3;

    }

}
