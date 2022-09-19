using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlpacaMyGames
{
    public static class Utilities
    {
        #region CreaterMethods
        /*****************/
        /*Creater Methods*/
        /*****************/

        public static TextMesh CreateTextObject(string text, Vector3 position, int fontSize = 20, TextAlignment textAlignment = default(TextAlignment), TextAnchor textAnchor = default(TextAnchor))
        {
            TextMesh textMesh = new GameObject("TextMesh", typeof(TextMesh)).GetComponent<TextMesh>();

            textMesh.transform.position = position;
            textMesh.fontSize = fontSize;
            textMesh.characterSize = 0.1f;
            textMesh.alignment = textAlignment;
            textMesh.anchor = textAnchor;

            textMesh.text = text;

            return textMesh;
        }

        #endregion

        #region Methods
        /*********/
        /*Methods*/
        /*********/

        public static int RandomInt(int maxExclusive)
        {
            return RandomInt(0, maxExclusive);
        }

        public static int RandomInt(int minInclusive, int maxExclusive)
        {
            return UnityEngine.Random.Range(minInclusive, maxExclusive);
        }

        public static float RandomFloat(float maxInclusive)
        {
            return RandomFloat(0.0f, maxInclusive);
        }

        public static float RandomFloat(float minInclusive, float maxInclusive)
        {
            return UnityEngine.Random.Range(minInclusive, maxInclusive);
        }

        public static LayerMask GetLayerMask(string layerName)
        {
            return LayerMask.NameToLayer(layerName);
        }

        public static Vector3 GetMouseWorldLocation()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        public static Vector3 GetMouseWorldLocation(Camera camera)
        {
            return camera.ScreenToWorldPoint(Input.mousePosition);
        }

        public static List<T> GetEnumListByType<T>()
        {
            List<T> newList = new List<T>();

            foreach (T enumObject in System.Enum.GetValues(typeof(T)))
                newList.Add(enumObject);

            return newList;
        }

        public static int GetEnumTypeAmount<T>()
        {
            int count = 0;

            foreach (T objectList in System.Enum.GetValues(typeof(T)))
                count++;

            return count;
        }

        public static string GetMonthName(int monthNumber)
        {
            string[] months = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            return months[monthNumber - 1];
        }

        public static string GetShortMonthName(int monthNumber)
        {
            string[] months = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

            return months[monthNumber - 1];
        }

        public static bool ChanceFunc(int percentageChance)
        {
            if (percentageChance == 0)
                return false;

            if (percentageChance == 100)
                return true;

            int randomNumber = Random.Range(0, 101);

            return randomNumber <= percentageChance;
        }

        public static bool ChanceFunc(float percentageChance)
        {
            if (percentageChance == 0.0f)
                return false;

            if (percentageChance == 100.0f)
                return true;

            float randomNumber = Random.Range(0.0f, 100.0f);
            return randomNumber <= percentageChance;
        }

        public static bool CheckObjectEnvironment(Transform objectToCheck, float environmentRadius, LayerMask objectsToAvoid)
        {
            float scale = objectToCheck.localScale.x;

            Collider2D[] hits = Physics2D.OverlapCircleAll(objectToCheck.position, environmentRadius + scale, objectsToAvoid);

            if (hits.Length > 1)
                return true;
            else
                return false;
        }

        public static Vector2 GetRandomWorldPosition(float shorteningFactor = 1.0f, Vector2 worldOriginPosition = default(Vector2))
        {
            Vector3 randomScreenPosition = Vector2.up * Screen.height * UnityEngine.Random.Range(0.0f, 1.0f) +
                        Vector2.right * Screen.width * UnityEngine.Random.Range(0.0f, 1.0f);
            Vector2 randomPosition = Camera.main.ScreenToWorldPoint(randomScreenPosition) / shorteningFactor;

            return worldOriginPosition + randomPosition;
        }

        public static Vector2 GetRandomScreenPosition(float borderScaling = 1.0f)
        {
            float x0y0 = 1.0f - borderScaling;

            return new Vector2(Random.Range(x0y0, 1.0f) * borderScaling * Screen.width, Random.Range(x0y0, 1.0f) * borderScaling * Screen.height);
        }

        public static bool IsInsideScreen(Vector2 position)
        {
            if (position.x >= 0.0f && position.x < Screen.width && position.y >= 0.0f && position.y < Screen.height)
                return true;
            else
                return false;
        }

        public static float GetWorldCameraRatio()
        {
            return (float)Camera.main.pixelWidth / (float)Camera.main.pixelHeight;
        }

        public static Vector2 GetWorldOrthographicCameraSize()
        {
            float cameraOrthographicHeight = Camera.main.orthographicSize;

            return new Vector2(cameraOrthographicHeight * GetWorldCameraRatio(), cameraOrthographicHeight);
        }

        public static Vector2 GetDistanceToNearestWorldEdges(Vector2 position)
        {
            Vector2 worldSize = GetWorldOrthographicCameraSize();
            if (position.x > 0.0f)
            {
                if (position.y > 0.0f)
                {
                    float vectorX = worldSize.x - position.x;
                    float vectorY = worldSize.y - position.y;
                    return new Vector2(vectorX, vectorY);
                }
                else if (position.y < 0.0f)
                {

                    float vectorX = worldSize.x - position.x;
                    float vectorY = worldSize.y - Mathf.Abs(position.y);
                    return new Vector2(vectorX, vectorY);
                }
                else
                {
                    float vectorX = worldSize.x - position.x;
                    float vectorY = worldSize.y;
                    return new Vector2(vectorX, vectorY);
                }
            }
            else if (position.x < 0.0f)
            {
                if (position.y > 0.0f)
                {
                    float vectorX = worldSize.x - Mathf.Abs(position.x);
                    float vectorY = worldSize.y - position.y;
                    return new Vector2(vectorX, vectorY);
                }
                else if (position.y < 0.0f)
                {
                    float vectorX = worldSize.x - Mathf.Abs(position.x);
                    float vectorY = worldSize.y - Mathf.Abs(position.y);
                    return new Vector2(vectorX, vectorY);
                }
                else
                {
                    float vectorX = worldSize.x - Mathf.Abs(position.x);
                    float vectorY = worldSize.y;
                    return new Vector2(vectorX, vectorY);
                }
            }
            else
            {
                if (position.y > 0.0f)
                {
                    float vectorX = worldSize.x;
                    float vectorY = worldSize.y - position.y;
                    return new Vector2(vectorX, vectorY);
                }
                else if (position.y < 0.0f)
                {
                    float vectorX = worldSize.x;
                    float vectorY = worldSize.y - Mathf.Abs(position.y);
                    return new Vector2(vectorX, vectorY);
                }
                else
                {
                    float vectorX = worldSize.x;
                    float vectorY = worldSize.y;
                    return new Vector2(vectorX, vectorY);
                }
            }
        }
#endregion

        #region Extensions
        /************/
        /*Extensions*/
        /************/

        public static void RemoveRandomElement<T>(this List<T> list)
        {
            int randomIndex = Random.Range(0, list.Count);
            list.RemoveAt(randomIndex);
        }

        public static T GetRandomElement<T>(this List<T> list)
        {
            if (list.Count == 0)
                return default(T);

            return list[Random.Range(0, list.Count)];
        }

        public static Vector3 InvertVector(this Vector3 vector)
        {
            return new Vector3(-vector.x, -vector.y, -vector.z);
        }

        public static Vector2 InvertVector(this Vector2 vector)
        {
            return new Vector2(-vector.x, -vector.y);
        }

        public static string Bold(this string word)
        {
            return "<b>" + word + "</b>";
        }

        public static string Italics(this string word)
        {
            return "<i>" + word + "</i>";
        }

        public static string Color(this string word, Color color)
        {
            string colorString = ColorUtility.ToHtmlStringRGBA(color);
            return "<color=#" + colorString + ">" + word + "</color>";
        }

        #endregion
    }
}
