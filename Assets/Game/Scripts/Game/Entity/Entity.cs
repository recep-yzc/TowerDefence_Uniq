using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Game.Entity
{
    public abstract class Entity<TEntity> where TEntity : Entity<TEntity>
    {
        private static Regex idRegex;

        private static readonly Dictionary<string, TEntity> Entities;

        private readonly TEntity _derived;

        static Entity()
        {
            idRegex = new Regex("^[A-Z0-9]{4,32}$",
                RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);
            Entities = new Dictionary<string, TEntity>();
            Ids = Array.Empty<string>();
        }

        protected Entity()
        {
            _derived = this as TEntity;
            if (_derived == null) throw new ArgumentException("Derived class type mismatch!", $"TEntity");
            Id = string.Empty;
            if (!Init()) throw new InvalidOperationException("Cannot initialize entity!");
        }

        private static string[] Ids { get; set; }

        private string Id { get; set; }

        public static TEntity Get(string id = "default") => !Entities.ContainsKey(id) ? null : Entities[id];

        public bool Register(string id = "default")
        {
            if (!string.IsNullOrEmpty(Id)) return false;
            if (!idRegex.IsMatch(id)) return false;
            if (Entities.ContainsKey(id)) return false;
            Entities.Add(id, _derived);
            Id = id;
            Ids = new string[Entities.Count];
            Entities.Keys.CopyTo(Ids, 0);
            return true;
        }

        public void UnRegister()
        {
            if (string.IsNullOrEmpty(Id)) return;
            if (Entities.Remove(Id))
            {
                Ids = new string[Entities.Count];
                Entities.Keys.CopyTo(Ids, 0);
            }

            Id = string.Empty;
        }

        public void Save()
        {
            if (string.IsNullOrEmpty(Id)) return;
            string text = $"__ENTITY({typeof(TEntity).Name})#{Id}";
            string text2 = JsonUtility.ToJson(_derived);
            if (!string.IsNullOrEmpty(text2)) PlayerPrefs.SetString(text, text2);
        }

        public void Load()
        {
            if (string.IsNullOrEmpty(Id)) return;
            string text = $"__ENTITY({typeof(TEntity).Name})#{Id}";
            string @string = PlayerPrefs.GetString(text);
            if (!string.IsNullOrEmpty(@string)) JsonUtility.FromJsonOverwrite(@string, _derived);
        }

        protected abstract bool Init();
    }
}