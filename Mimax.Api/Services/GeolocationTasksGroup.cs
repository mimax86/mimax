using System;
using System.Collections.Generic;

namespace Mimax.Api.Services
{
    public class GeolocationTasksGroup
    {
        public Guid Id { get; }

        public IEnumerable<GeolocationTask> Tasks { get; }

        public GeolocationTasksGroup(Guid id, IEnumerable<GeolocationTask> tasks)
        {
            Id = id;
            Tasks = tasks;
        }
    }
}