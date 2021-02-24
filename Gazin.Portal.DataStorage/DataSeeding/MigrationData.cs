using Gazin.Portal.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Gazin.Portal.DataStorage.DataSeeding
{
    public static class MigrationData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Holiday>().HasData(
                new Holiday { Id = 1, Uuid = Guid.NewGuid(), Date = new DateTime(2000, 1, 1), Description = "Confraternização Universal" },
                new Holiday { Id = 2, Uuid = Guid.NewGuid(), Date = new DateTime(2000, 4, 21), Description = "Tiradentes" },
                new Holiday { Id = 3, Uuid = Guid.NewGuid(), Date = new DateTime(2000, 5, 1), Description = "Dia Mundial do Trabalho" },
                new Holiday { Id = 4, Uuid = Guid.NewGuid(), Date = new DateTime(2000, 9, 7), Description = "Independência do Brasil" },
                new Holiday { Id = 5, Uuid = Guid.NewGuid(), Date = new DateTime(2000, 10, 12), Description = "Dia das crianças" },
                new Holiday { Id = 6, Uuid = Guid.NewGuid(), Date = new DateTime(2000, 11, 2), Description = "Finados" },
                new Holiday { Id = 7, Uuid = Guid.NewGuid(), Date = new DateTime(2000, 11, 15), Description = "Proclamação da República" },
                new Holiday { Id = 8, Uuid = Guid.NewGuid(), Date = new DateTime(2000, 12, 24), Description = "Véspera de Natal" },
                new Holiday { Id = 9, Uuid = Guid.NewGuid(), Date = new DateTime(2000, 12, 25), Description = "Natal" },
                new Holiday { Id = 10, Uuid = Guid.NewGuid(), Date = new DateTime(2000, 12, 31), Description = "Véspera de Ano Novo" }
            );
            //2, 3 e 4 de abril(sexta a domingo): Paixão de Cristo é dia 2

            modelBuilder.Entity<WorkdayOfWeek>().HasData(
                new WorkdayOfWeek { Id = 1, Uuid = Guid.NewGuid(), DayOfWeek = DayOfWeek.Sunday, Active = false },
                new WorkdayOfWeek { Id = 2, Uuid = Guid.NewGuid(), DayOfWeek = DayOfWeek.Monday, Active = true },
                new WorkdayOfWeek { Id = 3, Uuid = Guid.NewGuid(), DayOfWeek = DayOfWeek.Tuesday, Active = true },
                new WorkdayOfWeek { Id = 4, Uuid = Guid.NewGuid(), DayOfWeek = DayOfWeek.Wednesday, Active = true },
                new WorkdayOfWeek { Id = 5, Uuid = Guid.NewGuid(), DayOfWeek = DayOfWeek.Thursday, Active = true },
                new WorkdayOfWeek { Id = 6, Uuid = Guid.NewGuid(), DayOfWeek = DayOfWeek.Friday, Active = true },
                new WorkdayOfWeek { Id = 7, Uuid = Guid.NewGuid(), DayOfWeek = DayOfWeek.Saturday, Active = false }
            );
        }
    }
}