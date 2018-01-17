enum CardClass
{
    INVALID = 0,
    DEATHKNIGHT = 1,
    DRUID = 2,
    HUNTER = 3,
    MAGE = 4,
    PALADIN = 5,
    PRIEST = 6,
    ROGUE = 7,
    SHAMAN = 8,
    WARLOCK = 9,
    WARRIOR = 10,
    DREAM = 11,
    NEUTRAL = 12,
}

enum CardSet
{
    INVALID = 0,
    TEST_TEMPORARY = 1,
    CORE = 2,
    EXPERT1 = 3,
    HOF = 4,
    //REWARD = 4,
    MISSIONS = 5,
    DEMO = 6,
    NONE = 7,
    CHEAT = 8,
    BLANK = 9,
    DEBUG_SP = 10,
    PROMO = 11,
    //FP1 = 12,
    NAXX = 12,
    GVG = 13,
    //PE1 = 13,
    BRM = 14,
    //FP2 = 14,
    //PE2 = 15,
    TGT = 15,
    //TEMP1 = 15,
    CREDITS = 16,
    HERO_SKINS = 17,
    TB = 18,
    SLUSH = 19,
    LOE = 20,
    OG = 21,
    OG_RESERVE = 22,
    KARA = 23,
    KARA_RESERVE = 24,
    GANGS = 25,
    GANGS_RESERVE = 26,
    UNGORO = 27,
    ICECROWN = 1001,
    LOOTAPALOOZA = 1004
}

enum CardType
{
    INVALID = 0,
    GAME = 1,
    PLAYER = 2,
    HERO = 3,
    MINION = 4,
    SPELL = 5,
    ABILITY = 5,
    ENCHANTMENT = 6,
    WEAPON = 7,
    ITEM = 8,
    TOKEN = 9,
    HERO_POWER = 10,
}
enum Faction
{
    INVALID = 0,
    HORDE = 1,
    ALLIANCE = 2,
    NEUTRAL = 3,
}

enum FormatType
{
    FT_UNKNOWN = 0,
    FT_WILD = 1,
    FT_STANDARD = 2,
}
enum MultiClassGroup
{
    INVALID = 0,
    GRIMY_GOONS = 1,
    JADE_LOTUS = 2,
    KABAL = 3,
}
enum Race
{
    INVALID = 0,
    BLOODELF = 1,
    DRAENEI = 2,
    DWARF = 3,
    GNOME = 4,
    GOBLIN = 5,
    HUMAN = 6,
    NIGHTELF = 7,
    ORC = 8,
    TAUREN = 9,
    TROLL = 10,
    UNDEAD = 11,
    WORGEN = 12,
    GOBLIN2 = 13,
    MURLOC = 14,
    DEMON = 15,
    SCOURGE = 16,
    MECHANICAL = 17,
    ELEMENTAL = 18,
    OGRE = 19,
    BEAST = 20,
    PET = 20,
    TOTEM = 21,
    NERUBIAN = 22,
    PIRATE = 23,
    DRAGON = 24,
    BLANK = 25
}

enum Rarity
{
    INVALID = 0,
    COMMON = 1,
    FREE = 2,
    RARE = 3,
    EPIC = 4,
    LEGENDARY = 5,
    UNKNOWN_6 = 6,
}
