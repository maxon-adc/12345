﻿namespace GameStore.Web.Infrastructure.Abstract
{
    public interface IPipeline<T>
    {
        IPipeline<T> Register(IFilter<T> filter);

        T Process(T input);
    }
}
