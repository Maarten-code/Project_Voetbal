﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HensMaarten_GPRd1._2_DM_Project_DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class VoetbalEntities : DbContext
    {
        public VoetbalEntities()
            : base("name=VoetbalEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Competitie> Competitie { get; set; }
        public virtual DbSet<Functie> Functie { get; set; }
        public virtual DbSet<Positie> Positie { get; set; }
        public virtual DbSet<Speler> Speler { get; set; }
        public virtual DbSet<Stadion> Stadion { get; set; }
        public virtual DbSet<StadionTeam> StadionTeam { get; set; }
        public virtual DbSet<Team> Team { get; set; }
        public virtual DbSet<TeamCompetitie> TeamCompetitie { get; set; }
        public virtual DbSet<TeamSpeler> TeamSpeler { get; set; }
        public virtual DbSet<Trainer> Trainer { get; set; }
        public virtual DbSet<TrainerTeam> TrainerTeam { get; set; }
        public virtual DbSet<Wedstrijd> Wedstrijd { get; set; }
    }
}
