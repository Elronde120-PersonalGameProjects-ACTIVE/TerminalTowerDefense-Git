using System;
/// <summary>
/// A class to wrap around all needed resources in the game
/// </summary>
[System.Serializable]
public class Resources
{
    public int coin;

     public Resources(){
        coin = 0;
    } 
    public Resources(int startCoin){
        SetResources(startCoin);
    }   

    public Resources(Resources startResources){
        SetResources(startResources);
    }   

    void SetResources(int newCoinAmount){
         coin = newCoinAmount;
    }

    void SetResources(Resources resource){
        SetResources(resource.coin);
    }

    public void CopyResources(Resources resource){
        SetResources(resource);
    }
    
    public static bool operator <(Resources a, Resources b){
        return a.coin < b.coin;
    }

    public static bool operator <=(Resources a, Resources b){
        return a.coin <= b.coin;
    }


    public static bool operator >(Resources a, Resources b){
        return a.coin > b.coin;
    }

    public static bool operator >=(Resources a, Resources b){
        return a.coin >= b.coin;
    }

    public static Resources operator -(Resources a, Resources b){
        return new Resources(a.coin - b.coin);
    }

    public static Resources operator +(Resources a, Resources b){
        return new Resources(a.coin + b.coin);
    }

    public static Resources operator /(Resources a, int b)
    {
        if (b == 0)
        {
            throw new DivideByZeroException();
        }
        return new Resources(a.coin / b);
    }

    public static Resources operator *(Resources a, int b)
    {
        return new Resources(a.coin * b);
    }

}
