using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public GameObject showUiButton;
    public TextMeshProUGUI showUiText;
    public GameObject settingsPanel;
    public TextMeshProUGUI currentFunctionText;
    
    public Graph graph;

    private List<IGraphableFunction> functions = new List<IGraphableFunction>();

    private bool isSettingsShowing = false;
    private int currentFunctionIndex = 0;

    void Awake() {
        // Here we define the list of viewable functions
        functions.Add(new Function1());
        functions.Add(new Function2());
        functions.Add(new Function3());


        settingsPanel.SetActive(false);
    }

    public void OnSettingsToggled() {
        if (!isSettingsShowing) {
            // find out which function is currently showing
            string currentFunctionName = graph.renderedFunction.Name();
            currentFunctionIndex = 0;
            foreach (IGraphableFunction function in functions) {
                if (function.Name().Equals(currentFunctionName)) {
                    break;
                }
                currentFunctionIndex++;
            }
            currentFunctionText.SetText(currentFunctionName);

            // show settings
            settingsPanel.SetActive(true);
            showUiText.SetText("Done");
            isSettingsShowing = true;
        } else {
            // hide settings
            settingsPanel.SetActive(false);
            showUiText.SetText("Settings");
            isSettingsShowing = false;

            // update the graph renderer
            IGraphableFunction function = functions[currentFunctionIndex];
            Vector2[] recommendedDomain = function.GetRecommendedDomain();
            graph.RenderFunction(function, recommendedDomain[0], recommendedDomain[1], recommendedDomain[2], 50, 25);
        }
    }


    public void NextFunction() {
        SelectFunction((currentFunctionIndex + 1) % functions.Count);
    }

    public void PreviousFunction() {
        SelectFunction((currentFunctionIndex + functions.Count - 1) % functions.Count);
    }

    private void SelectFunction(int i) {
        currentFunctionIndex = i;
        currentFunctionText.SetText(functions[i].Name());
    }

}
