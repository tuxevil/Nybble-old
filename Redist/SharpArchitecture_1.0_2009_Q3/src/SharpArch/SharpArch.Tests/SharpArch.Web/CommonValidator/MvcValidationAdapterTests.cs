﻿using NUnit.Framework;
using System.Web.Mvc;
using SharpArch.Web.NHibernate;
using System.Collections.Generic;
using SharpArch.Core.CommonValidator;
using SharpArch.Core.NHibernateValidator.CommonValidatorAdapter;
using NHibernate.Validator.Engine;
using SharpArch.Web.CommonValidator;

namespace Tests.SharpArch.Web.CommonValidator
{
    [TestFixture]
    public class MvcValidationAdapterTests
    {
        [Test]
        public void CanTransferValidationMessagesToModelState() {
            ModelStateDictionary modelStateDictionary = new ModelStateDictionary();
            IList<IValidationResult> invalidValues = new List<IValidationResult>();
            invalidValues.Add(
                new ValidationResult(
                    new InvalidValue("Message 1", typeof(TransactionAttribute), "Property1", null, null)));
            invalidValues.Add(
                new ValidationResult(
                    new InvalidValue("Message 2", typeof(MvcValidationAdapter), "Property2", null, null)));
            invalidValues.Add(
                new ValidationResult(
                    new InvalidValue("Message 3", GetType(), "Property3", null, null)));

            MvcValidationAdapter.TransferValidationMessagesTo(modelStateDictionary, invalidValues);

            Assert.That(modelStateDictionary.Count, Is.EqualTo(3));
            Assert.That(modelStateDictionary["TransactionAttribute.Property1"].Errors[0].ErrorMessage,
                Is.EqualTo("Message 1"));
            Assert.That(modelStateDictionary["MvcValidationAdapter.Property2"].Errors[0].ErrorMessage,
                Is.EqualTo("Message 2"));
            Assert.That(modelStateDictionary["MvcValidationAdapterTests.Property3"].Errors[0].ErrorMessage,
                Is.EqualTo("Message 3"));
        }
    }
}
