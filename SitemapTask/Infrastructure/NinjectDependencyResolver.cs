﻿using Domain.Abstract;
using Domain.Concrete;
using Domain.Context;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SitemapTask.Infrastructure
{
	public class NinjectDependencyResolver : IDependencyResolver
	{
		private IKernel kernel;

		public NinjectDependencyResolver()
		{
			kernel = new StandardKernel();
			AddBindings();
		}

		public object GetService(Type serviceType)
		{
			return kernel.TryGet(serviceType);
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return kernel.GetAll(serviceType);
		}
		private void AddBindings()
		{
			kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>));
		}
	}
}