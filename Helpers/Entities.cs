using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Utils;

namespace EntitySubSystemBase;

public static class Entities {

        /// <summary>
        /// Fully checks if entity is valid
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>true if IsValid; NOT 0 and below max entities(32768)</returns>
        public static bool ValidateEntity(this CEntityInstance entity) {

            if (entity.IsValid is not true 
            || entity.Index <= 0
            || entity.Index >= 32768) return false;

            return true;
        }

        /// <summary>
        /// Fully checks if entity is valid and if it belongs to a player
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>true if IsValid; NOT 0;below 32768 and is player entity</returns>
        public static bool ValidatePotentialPlayerEntity(this CEntityInstance entity) {

            if (entity.IsValid is not true 
            || entity.Index <= 0
            || entity.Index >= 32768
            || entity.DesignerName.Contains("player") is not true) return false;

            return true;
        }

        /// <summary>
        /// Compare two Vectors and check if they collide
        /// </summary>
        /// <param name="entityPosition"></param>
        /// <param name="pointOfColision"></param>
        /// <returns></returns>
        public static bool Collides(Vector entityPosition, Vector pointOfColision) 
        {
            var distSrt = Vector3DistanceSquared(/*position of the entity's that has OnTouch*/entityPosition, pointOfColision);
            var radiusSquared = Math.Pow(40, 2);

            return distSrt < radiusSquared;

        }

        /// <summary>
        /// Calculate distance between two Vectors
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static float Vector3DistanceSquared(Vector a, Vector b)
        {
            float dx = a.X - b.X;
            float dy = a.Y - b.Y;
            float dz = a.Z - b.Z;

            return dx * dx + dy * dy + dz * dz;
        }

    }