## ThreadStaticWithAsyncAwait
This example demonstrates that we should not use ThreadStatic together with async/await because the callback delegate is executed on a different thread to one the async operation started on.
https://stackoverflow.com/questions/13010563/using-threadstatic-variables-with-async-await   

## Async Await ConfigureAwait
https://medium.com/bynder-tech/c-why-you-should-use-configureawait-false-in-your-library-code-d7837dce3d7f   
Looks that ConfigureAwait is needed only if we use Task.Wait function, if we use await then it is not needed

## Async Await in REST API Controllers
https://www.c-sharpcorner.com/article/async-await-and-asynchronous-programming-in-mvc/