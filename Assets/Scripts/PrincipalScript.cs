using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrincipalScript : MonoBehaviour
{
    public CanvasGroup upgradeClick, upgradeIdle, prestigeCanvas;

    public Text bacterysTotalesText, bacteryXSegText, clickPowerText;
    public double bacterysTotales, bacteryXSeg, clickPower = 1;

    //Variables costs and levels
    double costClick1 = 5, costClick2 = 1500, costClick3 = 10000;
    double costIdle1 = 50, costIdle2 = 1000, costIdle3 = 5000;
    int click1Level = 0, click2Level = 0, click3Level = 0;
    int idle1Level = 0, idle2Level = 0, idle3Level = 0;

    //Power upgrades
    double powerClick1 = 1, powerClick2 = 100, powerClick3 = 500;
    double powerIdle1 = 3, powerIdle2 = 50, powerIdle3 = 300;

    //TextUpgrades
    public Text upgradeClickText1, upgradeClickText2, upgradeClickText3;
    public Text upgradeIdleText1, upgradeIdleText2, upgradeIdleText3;

    //Virus prestige
    public Text virusText, virusGetsText;
    double virusCant = 0, bonus, virusGets = 0;
    // Start is called before the first frame update
    void Start()
    {
        CanvasChange(upgradeClick, false);
        CanvasChange(upgradeIdle, false);
        CanvasChange(prestigeCanvas, false);
        bacterysTotales = 0;
        bacteryXSeg = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        virusGets = (150 * System.Math.Sqrt(bacterysTotales / 10e10));
        bonus = 1 + virusCant * 0.05;
        
        bacterysTotales += bacteryXSeg * Time.deltaTime * bonus;
        #region informacion
        bacterysTotalesText.text = "Bacterys: " + ScientificDouble(bacterysTotalesText, bacterysTotales);
        bacteryXSegText.text = "Bacterys p/s: " + ScientificDouble(bacteryXSegText, bacteryXSeg);
        clickPowerText.text = "Click power: " + ScientificDouble(clickPowerText, clickPower);
        #endregion informacion
        Texto(upgradeClickText1, costClick1, powerClick1, click1Level);
        Texto(upgradeClickText2, costClick2, powerClick2, click2Level);
        Texto(upgradeClickText3, costClick3, powerClick3, click3Level);

        Texto(upgradeIdleText1, costIdle1, powerIdle1, idle1Level);
        Texto(upgradeIdleText2, costIdle2, powerIdle2, idle2Level);
        Texto(upgradeIdleText3, costIdle3, powerIdle3, idle3Level);
        virusText.text = "Virus: " + ScientificDouble(virusText, virusCant);
        virusGetsText.text = "Virus to get after prestige: " + ScientificDouble(virusGetsText, virusGets);


    }

    public void Clicker()
    {
        bacterysTotales += clickPower * bonus; 
    }
    #region interface
    public void CanvasChange(CanvasGroup x, bool y)
    {
        if (y)
        {
            x.alpha = 1;
            x.blocksRaycasts = true;
            x.interactable = true;
            return;
        }
            x.alpha = 0;
            x.blocksRaycasts = false;
            x.interactable = false;
    }

    public void openUpgradeClick()
    {
        CanvasChange(upgradeClick, true);
    }
    public void closeUpgradeClick()
    {
        CanvasChange(upgradeClick, false);
    }
    public void OpenUpgradeIdle()
    {
        CanvasChange(upgradeIdle, true);
    }
    public void CloseUpgradeIdle()
    {
        CanvasChange(upgradeIdle, false);
    }
    public void OpenPrestige()
    {
        CanvasChange(prestigeCanvas, true);
    }
    public void ClosePrestige()
    {
        CanvasChange(prestigeCanvas, false);
    }

    #endregion interface
    #region upgrades
    public void UpgradeClick1()
    {
        if(bacterysTotales >= costClick1)
        {
            bacterysTotales -= costClick1;
            clickPower += powerClick1;
            costClick1 *= 1.07;
            click1Level++;
        }
    }

    public void UpgradeClick2()
    {
        if (bacterysTotales >= costClick2)
        {
            bacterysTotales -= costClick2;
            clickPower += powerClick2;
            costClick2 *= 1.07;
            click2Level++;
        }
    }
    public void UpgradeClick3()
    {
        if (bacterysTotales >= costClick3)
        {
            bacterysTotales -= costClick3;
            clickPower += powerClick3;
            costClick3 *= 1.07;
            click3Level++;
        }
    }

    public void UpgradeIdle1()
    {
        if (bacterysTotales >= costIdle1)
        {
            bacterysTotales -= costIdle1;
            bacteryXSeg += powerIdle1;
            costIdle1 *= 1.07;
            idle1Level++;
        }
    }
    public void UpgradeIdle2()
    {
        if (bacterysTotales >= costIdle2)
        {
            bacterysTotales -= costIdle2;
            bacteryXSeg += powerIdle2;
            costIdle2 *= 1.07;
            idle2Level++;
        }
    }
    public void UpgradeIdle3()
    {
        if (bacterysTotales >= costIdle3)
        {
            bacterysTotales -= costIdle3;
            bacteryXSeg += powerIdle3;
            costIdle3 *= 1.07;
            idle3Level++;
        }
    }
    #endregion upgrades

    #region texto
    public void Texto(Text texto, double cost, double power, int level)
    {
        texto.text = "Cost: " + ScientificDouble(texto, cost) + " bac \n Power: " + power.ToString("F1") + "\n Level: " + level;
        
        
    }

    public string ScientificDouble(Text texto, double cost)
    {
        if(cost >= 1000)
        {
            var exponent = (System.Math.Floor(System.Math.Log10(System.Math.Abs(cost))));
            var mantissa = (cost / System.Math.Pow(10, exponent));
              
            return mantissa.ToString("F2") + "e" + exponent; 
        }
        else
        {
            texto.text = cost.ToString("F2");
            return cost.ToString("F2"); 
        }
    }

    public string ScientificFloat(Text texto, float cost)
    {
        if (cost >= 1000)
        {
            var exponent = (System.Math.Floor(System.Math.Log10(System.Math.Abs(cost))));
            var mantissa = (cost / System.Math.Pow(10, exponent));

            return mantissa.ToString("F2") + "e" + exponent; ;
        }
        else
        {
            texto.text = cost.ToString("F2");
            return cost.ToString("F2");
        }
    }
    #endregion texto
    #region prestige
    public void Prestige()
    {
        bacterysTotales = 0;
        bacteryXSeg = 0;
        clickPower = 1;
        costClick1 = 5;
        costClick2 = 500;
        costClick3 = 10000;
        costIdle1 = 50;
        costIdle2 = 1000;
        costIdle3 = 5000;
        click1Level = 0;
        click2Level = 0;
        click3Level = 0;
        idle1Level = 0;
        idle2Level = 0;
        idle3Level = 0;
        virusCant += virusGets;
    }
    #endregion prestige

}
