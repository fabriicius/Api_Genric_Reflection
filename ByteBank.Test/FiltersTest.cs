namespace ByteBank.Test;

[TestClass]
public class ControllerCambioTest
{
    [TestMethod]
    public void filterAtribute()
    {
        var typeClass = (new SubClass()).GetType();
        var retorno = typeClass.GetType("Any").GetCustomAttributes(true);
    }

    public class MainClass {
        public virtual void Any(){}
    }

    public class SubClass : MainClass {
        [AttributeClass]
        public override void Any(){}
    }

    public class AttributeClass : Attribute {
       
    }
}