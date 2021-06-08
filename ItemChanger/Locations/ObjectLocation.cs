﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ItemChanger.Util;
using SereCore;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ItemChanger.Locations
{
    public class ObjectLocation : IMutableLocation
    {
        public string objectName;
        public float elevation;
        public string sceneName { get; set; }
        public FlingType flingType { get; set; }
        public bool forceShiny { get; set; }

        public bool Supports(Container container)
        {
            switch (container)
            {
                case Container.Chest:
                case Container.GeoRock:
                case Container.GrubJar:
                    return !forceShiny;
                case Container.Shiny:
                    return true;
                default:
                    return false;
            }
        }

        public virtual void OnEnable(PlayMakerFSM fsm) { }
        public virtual void OnActiveSceneChanged() { }
        public virtual void Hook() { }
        public virtual void UnHook() { }

        public virtual void PlaceContainer(GameObject obj, Container containerType)
        {
            GameObject target = FindGameObject(objectName);
            ContainerUtility.ApplyTargetContext(target, obj, containerType, elevation);
            GameObject.Destroy(target);
        }


        public static GameObject FindGameObject(string objectName)
        {
            Scene currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();

            string[] objectHierarchy = objectName.Split('\\');
            int i = 1;
            GameObject obj = currentScene.FindGameObject(objectHierarchy[0]);
            while (i < objectHierarchy.Length)
            {
                obj = obj.FindGameObjectInChildren(objectHierarchy[i++]);
            }

            return obj;
        }
    }
}
