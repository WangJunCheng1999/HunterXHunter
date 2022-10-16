using System;


namespace GameFramework.Extensions {

    public class ErrorCheck {

        public static void NotNull(object obj,string name) {
            if( obj == null ) {
                throw new NullReferenceException($"object:{name} is null");
            }
        }

    }

}