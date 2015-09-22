//using System.Runtime.ExceptionServices;
//using System.Threading.Tasks;
//using ShortBus;


//namespace Library.Features.CardReader
//{
//    public class CheckedMediator : ICheckedMediator
//    {
//        private readonly IMediator _mediator;

//        public CheckedMediator(IMediator mediator)
//        {
//            _mediator = mediator;
//        }

//        public TResponseData SendRequest<TResponseData>(IRequest<TResponseData> request)
//        {
//            var result = _mediator.Request(request);

//            if (result.HasException())
//            {
//                var exception = result.Exception.InnerException ?? result.Exception;
//                ExceptionDispatchInfo.Capture(exception).Throw(); // ensure we throw the inner exception and keep the stack trace
//            }
//            return result.Data;
//        }

//        public async Task<TResponseData> SendRequestAsync<TResponseData>(IAsyncRequest<TResponseData> request)
//        {
//            var result = await _mediator.RequestAsync(request);

//            if (result.HasException())
//            {
//                var exception = result.Exception.InnerException ?? result.Exception;
//                ExceptionDispatchInfo.Capture(exception).Throw(); // ensure we throw the inner exception and keep the stack trace
//            }
//            return result.Data;
//        }


//        public Response Notify<TNotification>(TNotification notification)
//        {
//            throw new System.NotImplementedException();
//        }

//        public Task<Response> NotifyAsync<TNotification>(TNotification notification)
//        {
//            throw new System.NotImplementedException();
//        }
//    }
//}