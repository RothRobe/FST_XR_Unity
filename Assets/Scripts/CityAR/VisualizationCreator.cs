using System;
using DefaultNamespace;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;
using UnityEngine.Profiling.Experimental;

namespace CityAR
{
    public class VisualizationCreator : MonoBehaviour
    {

        public GameObject districtPrefab;
        public GameObject buildingPrefab;
        private DataObject _dataObject;
        private GameObject _platform;
        private Data _data;

        private void Start()
        {
            _platform = GameObject.Find("Platform");
            _data = _platform.GetComponent<Data>();
            _dataObject = _data.ParseData();
            BuildCity(_dataObject);
        }

        private void BuildCity(DataObject p)
        {
            if (p.project.files.Count > 0)
            {
                p.project.w = 1;
                p.project.h = 1;
                p.project.deepth = 1;
                BuildDistrict(p.project, false);
            }
        }

        private void BuildBuilding(Entry entry, float size)
        {
            if (entry == null)
            {
                return;
            }

            GameObject prefabInstance = Instantiate(buildingPrefab, entry.parentEntry.goc.transform, true);
            prefabInstance.name = entry.name;
            float height = entry.numberOfLines;
            Transform parent = prefabInstance.transform.parent.parent;
            prefabInstance.transform.localScale =
                new Vector3(size / parent.localScale.x, height / 10, size / parent.localScale.z);
            prefabInstance.transform.GetChild(0).gameObject.transform.localPosition = new Vector3(0, 0.5f, 0);
        }

        /*
         * entry: Single entry from the data set. This can be either a folder or a single file.
         * splitHorizontal: Specifies whether the subsequent children should be split horizontally or vertically along the parent
         */
        private void BuildDistrict(Entry entry, bool splitHorizontal)
        {
            if (entry.type.Equals("File"))
            {
                //TODO if entry is from type File, create building
                /*
                GameObject parent = GameObject.Find(entry.parentEntry.name);
                if (parent == null)
                {
                    Debug.Log("sad. Name: " + entry.parentEntry.name + "Base");
                    return;
                }
                GameObject cube = Instantiate(districtPrefab, parent.transform, true);
                cube.name = entry.name + ", Parent: " +  entry.parentEntry.name;
                cube.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.red;
                cube.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                //cube.transform.localPosition = new Vector3(entry.x, entry.deepth+0.001f, entry.z);
                Debug.Log(cube.transform.localPosition);*/
                
            }
            else
            {
                float x = entry.x;
                float z = entry.z;

                float dirLocs = entry.numberOfLines;
                entry.color = GetColorForDepth(entry.deepth);

                BuildDistrictBlock(entry, false);

                foreach (Entry subEntry in entry.files) {
                    subEntry.x = x;
                    subEntry.z = z;
                    
                    if (subEntry.type.Equals("Dir"))
                    {
                        float ratio = subEntry.numberOfLines / dirLocs;
                        subEntry.deepth = entry.deepth + 1;

                        if (splitHorizontal) {
                            subEntry.w = ratio * entry.w; // split along horizontal axis
                            subEntry.h = entry.h;
                            x += subEntry.w;
                        } else {
                            subEntry.w = entry.w;
                            subEntry.h = ratio * entry.h; // split along vertical axis
                            z += subEntry.h;
                        }
                    }
                    else
                    {
                        subEntry.parentEntry = entry;
                    }
                    BuildDistrict(subEntry, !splitHorizontal);
                }

                if (!splitHorizontal)
                {
                    entry.x = x;
                    entry.z = z;
                    if (ContainsDirs(entry))
                    {
                        entry.h = 1f - z;
                    }
                    entry.deepth += 1;
                    BuildDistrictBlock(entry, true);
                }
                else
                {
                    entry.x = -x;
                    entry.z = z;
                    if (ContainsDirs(entry))
                    {
                        entry.w = 1f - x;
                    }
                    entry.deepth += 1;
                    BuildDistrictBlock(entry, true);
                }
            }
        }

        /*
         * entry: Single entry from the data set. This can be either a folder or a single file.
         * isBase: If true, the entry has no further subfolders. Buildings must be placed on top of the entry
         */
        private void BuildDistrictBlock(Entry entry, bool isBase)
        {
            if (entry == null)
            {
                return;
            }
            
            float w = entry.w; // w -> x coordinate
            float h = entry.h; // h -> z coordinate
            
            if (w * h > 0)
            {
                GameObject prefabInstance = Instantiate(districtPrefab, _platform.transform, true);

                if (!isBase)
                {
                    prefabInstance.name = entry.name;
                    prefabInstance.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = entry.color;
                    prefabInstance.transform.localScale = new Vector3(entry.w, 1f,entry.h);
                    prefabInstance.transform.localPosition = new Vector3(entry.x, entry.deepth, entry.z);
                }
                else
                {
                    prefabInstance.name = entry.name+"Base";
                    prefabInstance.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
                    prefabInstance.transform.GetChild(0).rotation = Quaternion.Euler(90,0,0);
                    prefabInstance.transform.localScale = new Vector3(entry.w, 1,entry.h);
                    prefabInstance.transform.localPosition = new Vector3(entry.x, entry.deepth+0.001f, entry.z);
                    //prefabInstance.transform.GetChild(0).gameObject.AddComponent<GridObjectCollection>();
                }
                
                Vector3 scale = prefabInstance.transform.localScale;
                float scaleX = scale.x - (entry.deepth * 0.005f);
                float scaleZ = scale.z - (entry.deepth * 0.005f);
                float shiftX = (scale.x - scaleX) / 2f;
                float shiftZ = (scale.z - scaleZ) / 2f;
                prefabInstance.transform.localScale = new Vector3(scaleX, scale.y, scaleZ);
                Vector3 position = prefabInstance.transform.localPosition;
                prefabInstance.transform.localPosition = new Vector3(position.x - shiftX, position.y, position.z + shiftZ);
                if (isBase)
                {
                    float size = 0.02f;
                    float offset = 0.1f;
                    float buildinghöhe = size / prefabInstance.transform.localScale.z;
                    float buildingbreite = size / prefabInstance.transform.localScale.x;
                    prefabInstance.transform.GetChild(0).gameObject.AddComponent<GridObjectCollection>();
                    entry.goc = prefabInstance.transform.GetChild(0).GetComponent<GridObjectCollection>();
                    
                    entry.goc.CellWidth = size / prefabInstance.transform.localScale.x + 0.01f;
                    
                    if (h > w)
                    {
                        entry.goc.Layout = LayoutOrder.ColumnThenRow;
                        entry.goc.CellWidth = buildingbreite + offset;
                        entry.goc.CellHeight = size / prefabInstance.transform.localScale.z + 0.01f;
                        if ((buildingbreite + offset) * 3 > 1)
                        {
                            if ((buildingbreite + offset) * 2 > 1)
                            {
                                entry.goc.Columns = 1;
                            }
                            else
                            {
                                entry.goc.Columns = 2;
                            }
                        }
                    }
                    else
                    {
                        entry.goc.CellHeight = buildinghöhe + offset;
                        entry.goc.CellWidth = size / prefabInstance.transform.localScale.x + 0.01f;
                        if ((buildinghöhe + offset) * 3 > 1)
                        {
                            if ((buildinghöhe + offset) * 2 > 1)
                            {
                                entry.goc.Rows = 1;
                            }
                            else
                            {
                                entry.goc.Rows = 2;
                            }
                        }
                    }

                    entry.goc.SortType = CollationOrder.ChildOrder;
                    foreach (Entry file in entry.files)
                    {
                        if (file.type.Equals("File"))
                        {
                            BuildBuilding(file, size);
                        }
                    }
                    
                    entry.goc.UpdateCollection();
                }
            }
        }

        private bool ContainsDirs(Entry entry)
        {
            foreach (Entry e in entry.files)
            {
                if (e.type.Equals("Dir"))
                {
                    return true;
                }
            }

            return false;
        }
        
        private Color GetColorForDepth(int depth)
        {
            Color color;
            switch (depth)
            {
                case 1:
                    color = new Color(179f / 255f, 209f / 255f, 255f / 255f);
                    break;
                case 2:
                    color = new Color(128f / 255f, 179f / 255f, 255f / 255f);
                    break;
                case 3:
                    color = new Color(77f / 255f, 148f / 255f, 255f / 255f);
                    break;
                case 4:
                    color = new Color(26f / 255f, 117f / 255f, 255f / 255f);
                    break;
                case 5:
                    color = new Color(0f / 255f, 92f / 255f, 230f / 255f);
                    break;
                default:
                    color = new Color(0f / 255f, 71f / 255f, 179f / 255f);
                    break;
            }

            return color;
        }
    }
}