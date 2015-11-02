using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailAlmanac.Helpers {
    public static class ControllerHelpers {

        /// <summary>
        /// Updates entity with values from the view model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oldObject">Original Entity to be updated</param>
        /// <param name="newObject">Dummy Entity with updated values</param>
        /// <param name="binds">List of properties to be updated</param>
        /// <returns></returns>
        public static T UpdateEntity<T> (T oldObject, T newObject, List<string> binds) {
            foreach (var newProp in newObject.GetType().GetProperties()) {
                foreach (var oldProp in oldObject.GetType().GetProperties()) {
                    if (binds.Any(s => s.Contains(oldProp.Name)) && oldProp.Name == newProp.Name ) {
                        oldProp.SetValue(oldObject, newProp.GetValue(newObject));
                    }
                }
            }
            return oldObject;
        }
    }
}
