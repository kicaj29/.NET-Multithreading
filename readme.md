## ThreadStaticWithAsyncAwait
This example demonstrates that we should not use ThreadStatic together with async/await because the callback delegate is executed on a different thread to one the async operation started on.
https://stackoverflow.com/questions/13010563/using-threadstatic-variables-with-async-await