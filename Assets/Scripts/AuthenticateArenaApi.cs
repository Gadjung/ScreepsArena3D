﻿using Assets.Scripts.ScreepsArenaApi.Responses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.ScreepsArenaApi
{
    public class AuthenticateArenaApi : MonoBehaviour
    {
        private static AuthLoginResponse me = null;
        private Http http = new Http();

        public void Awake()
        {
            // Configure unity stacktrace log output
            //Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.None);
            //Application.SetStackTraceLogType(LogType.Warning, StackTraceLogType.None);
        }

        private void Start()
        {
            StartCoroutine(AuthenticateWithArenaApi());
        }

        public IEnumerator AuthenticateWithArenaApi()
        {
            Debug.Log("Waiting on steam");
            yield return new WaitForSecondsRealtime(10); // lets wait to make sure steam has launched

            while (AuthenticateSteam.SteamTicket == null)
            {
                Debug.Log("We have no steam ticket, waiting...");
                yield return new WaitForSecondsRealtime(10); // lets wait to make sure steam has launched
            }
            
            yield return http.ScreepsArenaLogin(AuthenticateSteam.SteamTicket, authResponse =>
            {
                me = authResponse;
                Debug.Log("Authenticated with arena, hello " + me.username);
            });

            // Should probably live somewhere else
            yield return SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
        }
    }
}
