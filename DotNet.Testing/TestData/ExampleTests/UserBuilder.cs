using System;
using System.Collections.Generic;
using System.Text;

namespace DotNet.Testing.TestData.ExampleTests
{ 
    public sealed class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = "";
        public bool IsActive { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }

    public sealed class UserBuilder : TestData.Builder<User>
    {
        private readonly TestData.SeededRandom _rnd;

        public UserBuilder(TestData.SeededRandom? rnd = null)
        {
            _rnd = rnd ?? new TestData.SeededRandom();
        }

        protected override User Create()
        {
            return new User
            {
                Id = Guid.NewGuid(),
                Email = $"{_rnd.NextString(8)}@example.com",
                IsActive = _rnd.NextBool(),
                CreatedAt = _rnd.NextDateTimeOffset(90)
            };
        }

        public UserBuilder Active()
            => (UserBuilder)With(u => u.IsActive = true);

        public UserBuilder WithEmail(string email)
            => (UserBuilder)With(u => u.Email = email);
    }
}