using FubuCore;
using FubuCore.Reflection;
using FubuValidation.Strategies;
using NUnit.Framework;

namespace FubuValidation.Tests.Strategies
{
    [TestFixture]
    public class when_evaluating_email_fields
    {
        private Accessor _accessor;
        private FieldRule _rule;
        private EmailFieldStrategy _strategy;
        private Notification _notification;
        private SimpleModel _model;

        [SetUp]
        public void BeforeEach()
        {
            _notification = new Notification();
            _model = new SimpleModel();
            _strategy = new EmailFieldStrategy();
            _accessor = AccessorFactory.Create<SimpleModel>(m => m.Email);
            _rule = new FieldRule(_accessor, new TypeResolver(), _strategy);
        }

        [Test]
        public void should_register_message_if_value_is_not_an_email()
        {
            _model.Email = "Invalid Value";
            _rule.Validate(_model, _notification);

            _notification
                .MessagesFor(_accessor)
                .Messages
                .ShouldHaveCount(1);
        }

        [Test]
        public void should_register_message_if_value_is_empty()
        {
            _model.Email = string.Empty;
            _rule.Validate(_model, _notification);

            _notification
                .MessagesFor(_accessor)
                .Messages
                .ShouldHaveCount(1);
        }

        [Test]
        public void should_register_message_if_value_is_null()
        {
            _model.Email = null;
            _rule.Validate(_model, _notification);

            _notification
                .MessagesFor(_accessor)
                .Messages
                .ShouldHaveCount(1);
        }

        [Test]
        public void should_register_message_if_value_has_an_embedded_email()
        {
            _model.Email = "This is an invalid value with a valid@email.com";
            _rule.Validate(_model, _notification);

            _notification
                .MessagesFor(_accessor)
                .Messages
                .ShouldHaveCount(1);
        }

        [Test]
        public void should_not_register_message_if_value_is_an_email()
        {
            _model.Email = "valid@email.com";
            _rule.Validate(_model, _notification);

            _notification
                .MessagesFor(_accessor)
                .Messages
                .ShouldHaveCount(0);
        }
    }
}