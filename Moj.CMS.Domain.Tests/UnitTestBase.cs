using Moj.CMS.Domain.Shared.Entities;
using Moj.CMS.Domain.Shared.Events;
using Moj.CMS.Domain.Shared.Exceptions;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Domain.Tests
{
    public class UnitTestBase
    {
        public static T AssertRegisteredSingleDomainEvent<T>(IAggregateRoot aggregate)
            where T : IDomainEvent
        {
            var domainEvent = aggregate.DomainEvents.ToList().OfType<T>().SingleOrDefault();
            ((IDomainEvent)domainEvent).ShouldNotBeNull();
            return domainEvent;
        }

        public static void AssertNotRegisteredDomainEvent<T>(IAggregateRoot aggregate)
            where T : IDomainEvent
        {
            var domainEvent = aggregate.DomainEvents.ToList().OfType<T>().FirstOrDefault();
            ((IDomainEvent)domainEvent).ShouldBeNull();
        }

        public static List<T> GetRegisteredDomainEvents<T>(IAggregateRoot aggregate)
            where T : IDomainEvent
        {
            var domainEvents = aggregate.DomainEvents.ToList().OfType<T>().ToList();
            return domainEvents;
        }

        public static void AssertBrokenRule<TRule>(Action action)
            where TRule : class, IBusinessRuleBase
        {
            var message = $"Expected {typeof(TRule).Name} broken rule";
            var businessRuleValidationException = Should.Throw<BusinessRuleValidationException>(action, message);
            businessRuleValidationException.ShouldNotBeNull();
            businessRuleValidationException.BrokenRule.ShouldBeOfType(typeof(TRule), message);
        }

        public static async Task AssertBrokenRuleAsync<TRule>(Task task)
            where TRule : class, IBusinessRuleBase
        {
            var message = $"Expected {typeof(TRule).Name} broken rule";
            var businessRuleValidationException = await Should.ThrowAsync<BusinessRuleValidationException>(task, message);
            businessRuleValidationException.ShouldNotBeNull();
            businessRuleValidationException.BrokenRule.ShouldBeOfType(typeof(TRule), message);
        }
    }
}