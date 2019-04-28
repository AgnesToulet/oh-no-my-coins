public class Tilesmaps {
    private static string[] levels = {
@"X X X X X X X X X X X X
X X X X X X X X X X X X
X X 0 0 0 0 0 0 0 0 X X
X X X X X X X X X 0 X X
X X 0 0 0 0 0 C X 0 X X
X X 0 X 0 X X X X 0 X X
X X 0 X 0 0 0 0 X 0 X X
X X 0 X 0 X X E X 0 X X
X X 0 X X X X X X 0 X X
X X 0 0 0 0 0 0 0 0 X X
X X X X X X X X X X X X
X X X X X X X X X X X X",
 
@"X X X X X X X X X X X X
X X X X X X X X X X X X
X X 0 0 0 0 0 0 0 0 X X
X X 0 X X X X X X 0 X X
X X 0 X 0 0 0 0 X 0 X X
X X 0 X 0 X 0 X X 0 X X
X X 0 X 0 X 0 X X 0 X X
X X 0 X C X E X X 0 X X
X X 0 X X X X X X 0 X X
X X 0 0 0 0 0 0 0 0 X X
X X X X X X X X X X X X
X X X X X X X X X X X X",

@"X X X X X X X X X X X X X X X X X X X X
X X X X X X X X X X X X X X X X X X X X
X X 0 X 0 0 0 X 0 0 0 X 0 0 0 X G 0 X X
X X 0 0 0 X 0 X 0 X X 0 0 X 0 X X 0 X X
X X X X X X 0 X 0 0 0 0 X X 0 0 X P X X
X X 0 0 0 0 0 X 0 X X X X X 0 0 0 0 X X
X X 0 X X X X 0 0 0 X X 0 X 0 X X X X X
X X 0 0 0 X 0 0 X 0 0 X 0 X 0 T 0 0 X X
X X X 0 X 0 0 X 0 0 X 0 0 X 0 X X 0 X X
X X 0 0 0 0 X 0 0 X 0 0 X X 0 X X 0 X X
X X X X 0 X 0 0 X X X 0 X X 0 X X 0 X X
X X 0 0 0 X X 0 0 0 X 0 0 0 0 X G 0 X X
X X 0 X X X X X X 0 X 0 X X X X X X X X
X X 0 0 X 0 0 0 X G X 0 0 0 0 0 0 0 X X
X X X 0 X 0 X 0 X X X X 0 X X 0 X 0 X X
X X 0 0 X 0 X 0 X 0 C X 0 X X X X 0 X X
X X 0 X X 0 X 0 0 0 X X 0 0 0 0 X X X X
X X 0 0 0 0 X 0 X 0 0 0 X 0 X 0 0 0 X X
X X X X X X X X X X X X X X X X X E X X
X X X X X X X X X X X X X X X X X X X X",

@"X X X X X X X X X X X X X X X X X X X X
X X X X X X X X X X X X X X X X X X X X
X X 0 0 0 0 0 X G X X 0 0 0 0 0 0 0 X X
X X 0 X X X 0 X 0 0 X 0 X X X X X 0 X X
X X 0 X 0 X 0 X X 0 X 0 X 0 0 0 X 0 X X
X X 0 X X X 0 0 0 0 X 0 X X X X X 0 X X
X X 0 0 0 0 0 X X X X 0 0 0 0 0 0 0 X X
X X X X 0 X 0 0 X 0 X 0 X X X X X 0 X X
X X 0 X 0 X X 0 X 0 X 0 X 0 0 0 X 0 X X
X X 0 X 0 0 X 0 0 0 X 0 X X X 0 X 0 X X
X X 0 X X 0 X 0 X 0 X 0 X X X 0 X 0 X X
X X 0 0 X 0 X 0 X 0 X 0 0 0 0 0 X 0 X X
X X X 0 0 0 X 0 X 0 X 0 X X X X X 0 X X
X X 0 0 X X X 0 X 0 X 0 X 0 0 0 0 0 X X
X X X 0 X 0 0 0 X P X 0 X 0 X X X X X X
X X 0 0 X 0 X X X 0 X 0 X 0 0 C 0 0 X X
X X X 0 X X X G X 0 X 0 X X X X X 0 X X
X X 0 0 0 0 0 0 X E X 0 0 0 0 0 X P X X
X X X X X X X X X X X X X X X X X X X X
X X X X X X X X X X X X X X X X X X X X",

@"X X X X X X X X X X X X X
X X X X X X X X X X X X X
X X 0 0 0 0 0 0 0 0 X X X
X X X X X X X X X 0 X X X
X X 0 0 0 0 0 0 0 0 C X X
X X E X X 0 X 0 X 0 X X X
X X X X X 0 X 0 X 0 X X X
X X 0 0 X 0 X 0 X 0 X X X
X X 0 0 X P X P X P X X X
X X X X X X X X X X X X X
X X X X X X X X X X X X X",

@"X X X X X X X X X X X X X X X X X X X
X X X X X X X X X X X X X X X X X X X
X X 0 0 0 0 0 0 0 0 0 0 0 0 0 X G X X
X X X 0 X 0 X X X X 0 X X X 0 0 0 X X
X X X 0 X 0 X T 0 0 0 0 0 X 0 X 0 X X
X X X 0 X 0 X X X X X X 0 X 0 X 0 X X
X X X P X 0 0 0 0 0 0 X 0 X 0 X X X X
X X X X X 0 X X X X 0 X 0 X 0 X 0 X X
X X 0 0 0 0 X T X X 0 X 0 X 0 X 0 X X
X X 0 X X X X 0 X 0 0 X 0 X 0 X 0 X X
X X G X 0 0 0 0 X 0 X 0 0 X 0 X 0 X X
X X X 0 0 X X X 0 0 X 0 0 X 0 X 0 X X
X X X 0 X 0 0 0 0 X 0 0 X 0 0 X 0 X X
X X 0 0 0 0 0 0 X 0 0 X 0 0 X 0 0 X X
X X 0 X X X X X X 0 X 0 0 X 0 0 X X X
X X 0 X 0 0 0 0 0 X 0 X 0 0 0 X G X X
X X 0 X 0 E X 0 0 X 0 X 0 X X 0 0 X X
X X C X 0 X 0 0 0 0 0 0 0 0 0 0 0 X X
X X X X X X X X X X X X X X X X X X X
X X X X X X X X X X X X X X X X X X X",

@"X X X X X X X X X X X X X X X X X X X X X X X X X X X X X X X X X X X X X X
X X X X X X X X X X X X X X X X X X X X X X X X X X X X X X X X X X X X X X
X X 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 X X X X
X X 0 0 0 0 0 0 0 0 0 0 0 0 0 0 P 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 X X X X
X X X X X X 0 0 X X X X 0 0 X X X X X X X X X X X X X X X X X X 0 0 X X X X
X X X X X X 0 0 X X X X 0 0 X X X X X X X X X X X X X X X X X X 0 0 X X X X
X X 0 0 0 0 0 0 X X 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 X X 0 0 0 0 X X X X
X X 0 0 0 0 0 0 X X G 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 X X 0 0 0 0 X X X X
X X 0 0 X X 0 0 X X X X X X X X X X X X X X X X X X 0 0 0 0 X X 0 0 X X X X
X X 0 0 X X 0 0 X X X X X X X X X X X X X X X X X X 0 0 0 0 X X 0 0 X X X X
X X 0 0 X X 0 0 X X 0 0 0 T 0 0 0 0 0 0 0 0 0 0 X X 0 0 X X X X 0 0 X X X X
X X 0 0 X X 0 0 X X 0 0 0 0 0 0 0 0 0 0 0 0 0 0 X X 0 0 X X X X 0 0 X X X X
X X 0 0 X X 0 0 X X 0 0 X X X X 0 0 X X X X 0 0 X X 0 0 0 0 X X 0 0 X X X X
X X 0 0 X X 0 0 X X 0 0 X X X X 0 0 X X X X 0 0 X X 0 0 0 0 X X 0 0 X X X X
X X 0 0 X X 0 0 X X X E X X 0 0 0 0 0 0 X X 0 0 X X X X 0 0 X X 0 0 0 0 X X
X X 0 0 X X 0 0 X X X X X X 0 0 0 0 0 0 X X 0 0 X X X X 0 0 X X 0 0 0 0 X X
X X 0 0 X X 0 0 0 0 0 G X X 0 0 C 0 0 0 X X 0 0 0 0 X X 0 0 0 0 X X 0 0 X X
X X 0 0 X X 0 0 0 0 0 0 X X 0 0 0 0 0 0 X X 0 0 0 0 X X 0 0 0 0 X X 0 0 X X
X X 0 0 X X 0 0 X X X X X X 0 0 0 0 0 0 X X X X 0 0 X X X X 0 0 X X 0 0 X X
X X 0 0 X X 0 0 X X X X X X 0 0 0 0 0 0 X X X X 0 0 X X X X 0 0 X X 0 0 X X
X X 0 0 X X X X 0 0 X X 0 0 X X X X X X X X X X 0 0 0 0 X X 0 0 X X 0 0 X X
X X 0 0 X X X X 0 0 X X 0 0 X X X X X X X X X X 0 0 0 0 X X 0 0 X X 0 0 X X
X X 0 0 X X 0 0 X X 0 0 0 0 X X 0 G X X X X X X X X 0 0 X X 0 0 X X 0 0 X X
X X 0 0 X X 0 0 X X 0 0 0 0 X X 0 0 X X X X X X X X 0 0 X X 0 0 X X 0 0 X X
X X 0 0 X X 0 0 X X 0 0 X X X X 0 0 X X 0 0 X X 0 0 0 0 X X 0 0 X X 0 0 X X
X X 0 0 X X 0 0 X X 0 0 X X X X 0 0 X X 0 0 X X 0 0 0 0 X X 0 0 X X 0 0 X X
X X 0 0 0 0 0 0 0 T 0 0 X X 0 0 0 0 X X 0 0 X X 0 0 X X 0 0 0 0 X X 0 0 X X
X X 0 0 0 0 0 0 0 0 0 0 X X 0 0 0 0 X X 0 0 X X 0 0 X X 0 0 0 0 X X 0 0 X X
X X 0 0 X X X X X X X X X X 0 0 X X 0 0 0 0 X X 0 0 0 0 0 0 0 0 X X 0 0 X X
X X 0 0 X X X X X X X X X X 0 0 X X 0 0 0 0 X X 0 0 0 0 0 0 0 0 X X 0 0 X X
X X 0 0 X X 0 0 0 0 0 0 0 0 0 0 0 0 X X P 0 X X X X X X X X X X X X 0 0 X X
X X 0 0 X X 0 0 0 0 0 0 0 0 0 0 0 0 X X 0 0 X X X X X X X X X X X X 0 0 X X
X X 0 0 0 0 0 0 X X X X X X X X 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 X X
X X 0 0 0 0 0 0 X X X X X X X X 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 X X
X X X X X X X X X X X X X X X X X X X X X X X X X X X X X X X X X X X X X X
X X X X X X X X X X X X X X X X X X X X X X X X X X X X X X X X X X X X X X"
  };

    private static int[] levelsMoney = { 30, 60, 60, 80, 30, 60, 80 };

    public static string GetLevel (int level) {
      return levels[level - 1];
    }

    public static int GetLevelMoney (int level) {
      return levelsMoney[level - 1];
    }
}