using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public static class Loader
{

    private class LoadingMB : MonoBehaviour { }

    public static Action onLoaderCallback;
    private static AsyncOperation loadingAsyncOperation;
    public enum Scene {
        StartMenu,
        Game,
        Loading,
        CheckPointTest,
    }

    //para carregar uma scene em outro script faça como no exemplo: Loader.Load(Loader.Scene.Game);
    public static void Load(Scene scene) {
        onLoaderCallback = () => {
            GameObject loadingGameObject = new GameObject("Loading Object");
            loadingGameObject.AddComponent<LoadingMB>().StartCoroutine(LoadSceneAsync(scene));
            LoadSceneAsync(scene);
        };

        SceneManager.LoadScene(Scene.Loading.ToString());

    }

    private static IEnumerator LoadSceneAsync(Scene scene){
        yield return null;

        loadingAsyncOperation = SceneManager.LoadSceneAsync(scene.ToString());

        //Fica preso aqui até carregar
        while(!loadingAsyncOperation.isDone){
            yield return null;
        }
    }

//retorna progresso do loading
    public static float GetLoadingProgress(){
        if(loadingAsyncOperation != null){
            return loadingAsyncOperation.progress;
        } else {
            return 1f;
        }
    }

//Automaticamente executado (triggered) no primeiro update depois que carregar a tela de loading.
// Faz callback para o Load carregar a tela objetivo
    public static void LoaderCallback(){
        
        if(onLoaderCallback != null){
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }

}
