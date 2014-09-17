
namespace Web.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.ServiceModel.Dispatcher;
    using System.ServiceModel.Channels;
    using System.ServiceModel;
    using System.Diagnostics;
    using System.ServiceModel.Description;

    #region WcfSilverlight Fault Behavior

    public class WcfSilverlightFaultBehavior : IDispatchMessageInspector
    {
        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            if (reply.IsFault)
            {
                HttpResponseMessageProperty property = new HttpResponseMessageProperty();

                // Here the response code is changed to 200.
                property.StatusCode = System.Net.HttpStatusCode.OK;

                reply.Properties[HttpResponseMessageProperty.Name] = property;
            }
        }

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            // Do nothing to the incoming message.
            return null;
        }
    };
    public sealed class WcfSilverlightFaultBehaviorAttribute : WcfBehaviorAttributeBase
    {
        public WcfSilverlightFaultBehaviorAttribute()
            : base(typeof(WcfSilverlightFaultBehavior))
        {
        }
    };

    #endregion

    #region Wcf Error Behavior

    public class WcfErrorBehavior : IErrorHandler
    {

        void IErrorHandler.ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            try
            {
                // Add code here to build faultreason for client based on exception
                FaultReason faultReason = new FaultReason(error.Message);
                ExceptionDetail exceptionDetail = new ExceptionDetail(error);

                // For security reasons you can also decide to not give the ExceptionDetail back to the client or change the message, etc
                FaultException<ExceptionDetail> faultException = new FaultException<ExceptionDetail>(exceptionDetail, faultReason, FaultCode.CreateSenderFaultCode(new FaultCode("0")));

                MessageFault messageFault = faultException.CreateMessageFault();
                fault = Message.CreateMessage(version, messageFault, faultException.Action);
            }
            catch
            {
                // Todo log error
            }
        }

        /// <summary>
        /// Handle all WCF Exceptions
        /// </summary>
        bool IErrorHandler.HandleError(Exception ex)
        {
            try
            {
                // Add logging of exception here!
                Debug.WriteLine(ex.ToString());
            }
            catch
            {
                // Todo log error
            }

            // return true means we handled the error.
            return true;
        }

    };
    public sealed class WcfErrorBehaviorAttribute : WcfBehaviorAttributeBase
    {
        public WcfErrorBehaviorAttribute()
            : base(typeof(WcfErrorBehavior))
        {
        }
    }

    #endregion

    #region Wcf Behavior Attribute Base

    public abstract class WcfBehaviorAttributeBase : Attribute, IServiceBehavior
    {
        private Type _behaviorType;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="typeBehavior">IDispatchMessageInspector, IErrorHandler of IParameterInspector</param>
        public WcfBehaviorAttributeBase(Type typeBehavior)
        {
            _behaviorType = typeBehavior;
        }

        void IServiceBehavior.AddBindingParameters(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            object behavior;
            try
            {
                behavior = Activator.CreateInstance(_behaviorType);
            }
            catch (MissingMethodException e)
            {
                throw new ArgumentException("The Type specified in the BehaviorAttribute constructor must have a public empty constructor.", e);
            }
            catch (InvalidCastException e)
            {
                throw new ArgumentException("The Type specified in the BehaviorAttribute constructor must implement IDispatchMessageInspector, IParamaterInspector of IErrorHandler", e);
            }

            foreach (ChannelDispatcher channelDispatcher in serviceHostBase.ChannelDispatchers)
            {
                if (behavior is IParameterInspector)
                {
                    foreach (EndpointDispatcher epDisp in channelDispatcher.Endpoints)
                    {
                        foreach (DispatchOperation op in epDisp.DispatchRuntime.Operations)
                            op.ParameterInspectors.Add((IParameterInspector)behavior);
                    }
                }
                else if (behavior is IErrorHandler)
                {
                    channelDispatcher.ErrorHandlers.Add((IErrorHandler)behavior);
                }
                else if (behavior is IDispatchMessageInspector)
                {
                    foreach (EndpointDispatcher endpointDispatcher in channelDispatcher.Endpoints)
                    {
                        endpointDispatcher.DispatchRuntime.MessageInspectors.Add((IDispatchMessageInspector)behavior);
                    }
                }
            }

        }

        void IServiceBehavior.Validate(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
        }

    };

    #endregion

}
