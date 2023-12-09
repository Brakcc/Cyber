namespace GameContent.Entity.Unit.KapasGen
{
    public static class Damage
    {
        /// <summary>
        /// Damage done by a Hacker. Does Not consider the defense value
        /// </summary>
        /// <param name="atkValue"></param>
        /// <returns></returns>
        public static float HackerDamage(int atkValue) => atkValue;

        /// <summary>
        /// Damage done by DPS and Tank. Consider the def value of the attacked Unit
        /// </summary>
        /// <param name="atkValue"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static float NormalDamage(int atkValue, int defValue) =>
            10 + (atkValue / (0.5f * defValue + 10)) * 15 - defValue / 5;

        /// <summary>
        /// /!\ Critical /!\ Damage done by DPS and Tank. Consider the def value of the attacked Unit
        /// </summary>
        /// <param name="atkValue"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static float CritDamage(int atkValue, int defValue) =>
            (10 + (atkValue / (0.5f * defValue + 10)) * 15 - defValue / 5) * 1.5f;
    }
}
