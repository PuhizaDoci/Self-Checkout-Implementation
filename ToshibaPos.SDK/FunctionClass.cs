using System;

namespace ToshibaPos.SDK
{
    public static class FunctionClass
    {
        public static void CopyClass(Object source, object destination)
        {
            var srcT = source.GetType();
            var dstT = destination.GetType();
            foreach (var f in srcT.GetFields())
            {
                var dstF = dstT.GetField(f.Name);
                if (dstF == null)
                    continue;
                dstF.SetValue(destination, f.GetValue(source));
            }

            foreach (var f in srcT.GetProperties())
            {
                var dstF = dstT.GetProperty(f.Name);
                if (dstF == null)
                    continue;
                if (f.CanWrite)
                    dstF.SetValue(destination, f.GetValue(source, null), null);
            }
        }
    }
}
