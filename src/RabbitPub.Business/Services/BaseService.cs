﻿using System.Globalization;
using System.Resources;
using AutoMapper;
using RabbitPub.Business.Utils;

namespace RabbitPub.Business.Services;

public abstract class BaseService
{
    protected readonly ResourceSet ResourceSet;
    protected readonly IMapper Mapper;

    protected BaseService(
        IMapper mapper,
        ResourceManager resourceManager,
        CultureInfo cultureInfo)
    {
        Mapper = mapper;
        ResourceSet = resourceManager.GetResourceSet(cultureInfo, true, true);
    }

    protected void ReturnResourceError<TException>(string nameResource, params object[] parameters)
        where TException : Exception
    {
        var mensagem = parameters.Length > default(int)
            ? ResourceSet.GetString(nameResource)!.ResourceFormat(parameters)
            : ResourceSet.GetString(nameResource);

        throw (Activator.CreateInstance(typeof(TException), mensagem) as TException)!;
    }

    protected void ReturnError<TException>(string message) where TException : Exception
    {
        throw (Activator.CreateInstance(typeof(TException), message) as TException)!;
    }
}