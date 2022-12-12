

using ByteBank.Portal.Infraestrutura.Ioc;

namespace ByteBank.Portal.Infraestrutura;

    public class ControllerResolver
    {
        private readonly IContainer _container;

        public ControllerResolver(IContainer container)
        {
            _container = container;
        }

        public object GetController(string nameController)
        {
             var typeController = Type.GetType(nameController);
            //var typeController = nameController.GetType();
            var instanceController = _container.Recovery(typeController);
            return instanceController;
        }
    }
