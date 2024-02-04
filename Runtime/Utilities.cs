using System;
using System.Collections.Generic;
using System.Text;
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

        public static void DrawLineSegment(List<Vector2> points)
        {
            float duration = 10.0f;
            DrawLineSegment(points, UnityEngine.Color.white, duration);
        }

        public static void DrawLineSegment(List<Vector2> points, Color color, float duration)
        {
            if (points == null)
                return;

            for (int i = 0; i < points.Count - 1; i++)
            {
                Vector2 current = points[i];
                Vector2 neighbor = points[i + 1];

                Debug.DrawLine(current, neighbor, color, duration);
            }
        }

        /// <summary>
        /// Returns a string representation of an <see cref="Enum"/> value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumElement"></param>
        /// <returns><see cref="string"/></returns>
        public static string EnumToString<T>(T enumElement) where T : Enum
        {
            string enumString = enumElement.ToString();
            string loweredEnumString = enumString.ToLower();
            char spaceCharacter = ' ';
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < enumString.Length; i++)
            {
                if (i != 0 && enumString[i] != loweredEnumString[i])
                    sb.Append(spaceCharacter);

                if (i != 0 && char.IsDigit(enumString[i]))
                    sb.Append(spaceCharacter);

                sb.Append(enumString[i]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Returns a randomly oriented <see cref="Vector3"/> with a random length up to <paramref name="maxRadius"/>
        /// </summary>
        /// <param name="maxRadius"></param>
        /// <param name="includeZ"></param>
        /// <returns>Randomized <see cref="Vector3"/></returns>
        public static Vector3 GetRandomLengthVector3(float maxRadius = 1.0f, bool includeZ = true)
        {
            Vector3 randomVector = new Vector3(
                UnityEngine.Random.Range(-1.0f, 1.0f) * maxRadius,
                UnityEngine.Random.Range(-1.0f, 1.0f) * maxRadius,
                0.0f);

            if (includeZ)
                randomVector.z = UnityEngine.Random.Range(-1.0f, 1.0f) * maxRadius;

            return randomVector;
        }

        /// <summary>
        /// Returns a randomly oriented <see cref="Vector3"/> with length <paramref name="randomRadius"/>, which is 1 by default
        /// </summary>
        /// <param name="randomRadius"></param>
        /// <returns>Randomized <see cref="Vector3"/></returns>
        public static Vector3 GetRandomVector3(float randomRadius = 1.0f, bool includeZ = true)
        {
            Vector3 randomVector = 
                new Vector3(
                    UnityEngine.Random.Range(-1.0f, 1.0f), 
                    UnityEngine.Random.Range(-1.0f, 1.0f), 
                    0.0f).normalized * randomRadius;

            if (includeZ)
                randomVector.z = UnityEngine.Random.Range(-1.0f, 1.0f);

            return randomVector;
        }

        /// <summary>
        /// Returns a randomly oriented <see cref="Vector2"/> with length <paramref name="randomRadius"/>, which is 1 by default
        /// </summary>
        /// <param name="randomRadius"></param>
        /// <returns>Randomized <see cref="Vector2"/></returns>
        public static Vector2 GetRandomVector2(float randomRadius = 1.0f)
        {
            Vector2 randomVector = 
                new Vector2(
                    UnityEngine.Random.Range(-1.0f, 1.0f), 
                    UnityEngine.Random.Range(-1.0f, 1.0f)).normalized * randomRadius;
            return randomVector;
        }

        /// <summary>
        /// Returns a random <see cref="Enum"/> value of type <typeparamref name="T"/> where one value is excluded
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="excluding"></param>
        /// <returns><see cref="Enum"/> of tpye <typeparamref name="T"/></returns>
        public static T GetRandomEnumValueExcluding<T>(T excluding) where T : Enum
        {
            Type type = typeof(T);
            Array values = Enum.GetValues(type);
            List<T> listOfValues = new List<T>(values.Length);
            foreach (T element in values)
            {
                if (element.Equals(excluding))
                    continue;

                listOfValues.Add(element);
            }

            return listOfValues.GetRandomElement();
        }

        /// <summary>
        /// Returns a random <see cref="Enum"/> value of type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns><see cref="Enum"/> of type <typeparamref name="T"/></returns>
        public static T GetRandomEnumValue<T>() where T : Enum
        {
            Type type = typeof(T);
            Array values = Enum.GetValues(type);
            return (T)values.GetValue(RandomInt(values.Length));
        }

        /// <summary>
        /// Returns the number of elements of an <see cref="Enum"/> of type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Length as <see cref="int"/></returns>
        public static int GetEnumLength<T>()
        {
            return Enum.GetValues(typeof(T)).Length;
        }

        public static List<T> GetListOfObjectsFromContainer<T>(Transform containerTransform, string subcontainerName = "")
        {
            if (containerTransform == null)
                return new List<T>();

            if (subcontainerName.Equals(""))
                return new List<T>(containerTransform.GetComponentsInChildren<T>());

            Transform subcontainer = containerTransform.Find(subcontainerName);

            if (subcontainer == null)
                return new List<T>();

            return new List<T>(subcontainer.GetComponentsInChildren<T>());
        }

        //0 degree is the positive x axis (right), 90 degrees is the positive y axis (up)
        public static Vector2 GetVectorFromAngle(float angleInDegrees)
        {
            return Vector2.up * Mathf.Sin(angleInDegrees * Mathf.Deg2Rad) + Vector2.right * Mathf.Cos(angleInDegrees * Mathf.Deg2Rad);
        }

        /// <summary>
        /// Returns a Fibonacci sequence up to a number
        /// </summary>
        /// <param name="number"></param>
        /// <returns><see cref="Array"/> of <see cref="int"/></returns>
        public static int[] FibonacciTo(int number)
        {
            int[] sequence = new int[number];

            for (int i = 0; i < number; i++)
                sequence[i] = FibonacciAt(i);

            return sequence;
        }

        /// <summary>
        /// Returns a Fibonacci sequence member at number
        /// </summary>
        /// <param name="number"></param>
        /// <returns><see cref="int"/></returns>
        public static int FibonacciAt(int number)
        {
            if (number == 0)
                return 0;

            if (number == 1)
                return 1;

            return FibonacciAt(number - 1) + FibonacciAt(number - 2);
        }

        public static int Factorial(int number)
        {
            if (number == 0)
                return 1;

            return number * Factorial(number - 1);
        }

        /// <summary>
        /// Returns a random integer from 0 to <paramref name="maxExclusive"/>
        /// </summary>
        /// <param name="maxExclusive"></param>
        /// <returns><see cref="int"/></returns>
        public static int RandomInt(int maxExclusive)
        {
            return RandomInt(0, maxExclusive);
        }

        /// <summary>
        /// Returns a random integer from <paramref name="minInclusive"/> to <paramref name="maxExclusive"/>
        /// </summary>
        /// <param name="minInclusive"></param>
        /// <param name="maxExclusive"></param>
        /// <returns><see cref="int"/></returns>
        public static int RandomInt(int minInclusive, int maxExclusive)
        {
            return UnityEngine.Random.Range(minInclusive, maxExclusive);
        }

        /// <summary>
        /// Returns a random float from 0 to <paramref name="maxInclusive"/>
        /// </summary>
        /// <param name="maxInclusive"></param>
        /// <returns><see cref="float"/></returns>
        public static float RandomFloat(float maxInclusive)
        {
            return RandomFloat(0.0f, maxInclusive);
        }

        /// <summary>
        /// Returns a random float from <paramref name="minInclusive"/> to <paramref name="maxInclusive"/>
        /// </summary>
        /// <param name="minInclusive"></param>
        /// <param name="maxInclusive"></param>
        /// <returns><see cref="float"/></returns>
        public static float RandomFloat(float minInclusive, float maxInclusive)
        {
            return UnityEngine.Random.Range(minInclusive, maxInclusive);
        }

        public static LayerMask GetLayerMask(string layerName)
        {
            return LayerMask.NameToLayer(layerName);
        }

        public static Vector2 GetMouseWorldLocation2D()
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return position;
        }

        public static Vector3 GetMouseWorldLocation()
        {
            Vector2 location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return location;
        }

        public static Vector3 GetMouseWorldLocation(Camera camera)
        {
            return camera.ScreenToWorldPoint(Input.mousePosition);
        }

        /// <summary>
        /// Returns a list of enums of type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns><see cref="List{T}"/> of enums</returns>
        public static List<T> GetEnumListByType<T>()
        {
            List<T> newList = new List<T>();

            foreach (T enumObject in System.Enum.GetValues(typeof(T)))
                newList.Add(enumObject);

            return newList;
        }

        /// <summary>
        /// Returns the number of members of an enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns><see cref="int"/></returns>
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

        /// <summary>
        /// Integer chance function. Input is integer percentage chance, output is true if the chance is hit, false otherwise.
        /// </summary>
        /// <param name="percentageChance"></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ChanceFunc(int percentageChance)
        {
            if (percentageChance == 0)
                return false;

            if (percentageChance == 100)
                return true;

            int randomNumber = UnityEngine.Random.Range(0, 101);

            return randomNumber <= percentageChance;
        }

        /// <summary>
        /// Float chance function. Input is float percentage chance, output is true if the chance is hit, false otherwise.
        /// </summary>
        /// <param name="percentageChance"></param>
        /// <returns><see cref="bool"/></returns>
        public static bool ChanceFunc(float percentageChance)
        {
            if (percentageChance == 0.0f)
                return false;

            if (percentageChance == 100.0f)
                return true;

            float randomNumber = UnityEngine.Random.Range(0.0f, 100.0f);
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

        public static List<Vector2> GetListOfRandom2DLocations(int numberOfLocations, float emptyRadiusAroundLocation = 0.0f, float borderMargin = 0.0f)
        {
            List<Vector2> listOfLocations = new List<Vector2>();

            int iterations = 0;
            for (int i = 0; i < numberOfLocations; i++)
            {
                iterations++;

                Vector2 position = GetRandomWorldPositionFromScreen(borderMargin);

                bool positionsTooClose = false;
                float iteratedRadius = (iterations > numberOfLocations * 2) ? emptyRadiusAroundLocation * 0.5f : emptyRadiusAroundLocation;
                foreach (Vector2 location in listOfLocations)
                {
                    if (Vector2.Distance(location, position) < iteratedRadius)
                    {
                        positionsTooClose = true;
                        break;
                    }
                }

                if (!positionsTooClose)
                    listOfLocations.Add(position);
                else
                    i--;

                if (iterations > numberOfLocations * 3)
                    break;
            }

            if (listOfLocations.Count < numberOfLocations)
                Debug.LogError((numberOfLocations - listOfLocations.Count) + " locations not possible to spawn. Decrease the number or the empty radius!");

            return listOfLocations;
        }

        public static Vector2 GetRandomXBoundaryPosition(float margin = 0.0f, int side = 1)
        {
            Vector2 cameraOrthographicSize = GetWorldOrthographicCameraSize();
            Vector2 position = new Vector2();

            position.x = side * cameraOrthographicSize.x;
            if (position.x > 0.0f)
                position.x += margin;
            else
                position.x -= margin;
            position.y = UnityEngine.Random.Range(-cameraOrthographicSize.y, cameraOrthographicSize.y);

            return position;
        }

        public static Vector2 GetRandomYBoundaryPosition(float margin = 0.0f, int side = 1)
        {
            Vector2 cameraOrthographicSize = GetWorldOrthographicCameraSize();
            Vector2 position = new Vector2();

            position.y = side * cameraOrthographicSize.y;
            if (position.y > 0.0f)
                position.y += margin;
            else
                position.y -= margin;
            position.x = UnityEngine.Random.Range(-cameraOrthographicSize.x, cameraOrthographicSize.x);

            return position;
        }

        public static Vector2 GetRandomBoundaryPosition(Vector2 margin = default(Vector2))
        {
            Vector2 cameraOrthographicSize = GetWorldOrthographicCameraSize();
            Vector2 position = new Vector2();

            if (ChanceFunc(50))
            {
                position.x = ChanceFunc(50) ? -cameraOrthographicSize.x : cameraOrthographicSize.x;
                if (position.x > 0.0f)
                    position.x += margin.x;
                else
                    position.x -= margin.x;
                position.y = UnityEngine.Random.Range(-cameraOrthographicSize.y, cameraOrthographicSize.y);
            }
            else
            {
                position.x = UnityEngine.Random.Range(-cameraOrthographicSize.x, cameraOrthographicSize.x);
                position.y = ChanceFunc(50) ? -cameraOrthographicSize.y : cameraOrthographicSize.y;
                if (position.y > 0.0f)
                    position.y += margin.y;
                else
                    position.y -= margin.y;
            }

            return position;
        }

        public static Vector2 GetWorldPositionFromScreen2D(Vector2 screenPosition)
        {
            return Camera.main.ScreenToWorldPoint(screenPosition);
        }

        public static Vector2 GetWorldPositionFromScreen2D(Vector2 screenPosition, Camera camera)
        {
            return camera.ScreenToWorldPoint(screenPosition);
        }

        public static Vector2 GetRandomWorldPositionFromScreen(float borderMargin = 0.0f, Vector2 worldOriginPosition = default(Vector2))
        {
            Vector3 randomScreenPosition = GetRandomScreenPosition(borderMargin);
            Vector2 randomPosition = Camera.main.ScreenToWorldPoint(randomScreenPosition);

            return worldOriginPosition + randomPosition;
        }

        public static Vector2 GetScreenPositionFromWorld2D(Vector2 worldPosition)
        {
            return Camera.main.WorldToScreenPoint(worldPosition);
        }

        public static Vector2 GetRandomScreenPosition(float borderMargin = 0.0f)
        {
            if (borderMargin < 0.0f || borderMargin > 0.5f)
            {
                Debug.LogError("Border margin should be from 0.0f to 0.5f!");
                return Vector2.zero;
            }

            return new Vector2(
                UnityEngine.Random.Range(borderMargin, 1.0f - borderMargin) * Screen.width,
                UnityEngine.Random.Range(borderMargin, 1.0f - borderMargin) * Screen.height
                );
        }

        public static bool IsInsideWorldScreen(Vector2 position, Vector2 worldMarginXY = default(Vector2))
        {
            Vector2 cameraOrthographicSize = GetWorldOrthographicCameraSize();
            return position.x > -1 * (worldMarginXY.x + cameraOrthographicSize.x) &&
                   position.x < +1 * (worldMarginXY.x + cameraOrthographicSize.x) &&
                   position.y < +1 * (worldMarginXY.y + cameraOrthographicSize.y) &&
                   position.y > -1 * (worldMarginXY.y + cameraOrthographicSize.y);
        }

        public static bool IsInsideScreen(Vector2 position)
        {
            return position.x >= 0.0f && position.x < Screen.width && position.y >= 0.0f && position.y < Screen.height;
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

        public static void SpawnRandomly(this GameObject gameObject, int spawnChance)
        {
            if (!ChanceFunc(spawnChance))
                UnityEngine.Object.Destroy(gameObject);
        }

        public static float GetRandom(this Vector2 vector)
        {
            return UnityEngine.Random.Range(vector.x, vector.y);
        }

        public static int GetRandom(this Vector2Int vector)
        {
            return UnityEngine.Random.Range(vector.x, vector.y);
        }

        public static void DestroyObject(this Transform transform, float destroyAfter = 0.0f)
        {
            UnityEngine.Object.Destroy(transform.gameObject, destroyAfter);
        }

        public static Transform AddNewGameObject(this Transform transform, string objectName)
        {
            if (transform.Find(objectName) != null)
                return transform.Find(objectName);

            Transform newTransform = new GameObject(objectName).transform;
            newTransform.SetParent(transform);

            return newTransform;
        }

        /// <summary>
        /// If given element already exists in a list it returns false and does not add the element to the list
        /// <br>Otherwise it adds the element to the list and returns true</br>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="element"></param>
        /// <returns><see cref="bool"/></returns>
        public static bool AddIfNone<T>(this List<T> list, T element)
        {
            if (list.Contains(element))
                return false;

            list.Add(element);
            return true;
        }

        public static float SumAll(this List<float> list)
        {
            float sum = 0.0f;
            list.ForEach(x => { sum += x; });
            return sum;
        }

        public static int SumAll(this List<int> list)
        {
            int sum = 0;
            list.ForEach(x => { sum += x; });
            return sum;
        }

        public static void RemoveRandomElement<T>(this List<T> list)
        {
            int randomIndex = UnityEngine.Random.Range(0, list.Count);
            list.RemoveAt(randomIndex);
        }

        public static T GetRandomElement<T>(this List<T> list)
        {
            if (list == null)
                return default(T);

            if (list.Count == 0)
                return default(T);

            return list[UnityEngine.Random.Range(0, list.Count)];
        }

        public static float Sum(this Vector2 vector2)
        {
            return vector2.x + vector2.y;
        }

        public static float Sum(this Vector3 vector3)
        {
            return vector3.x + vector3.y + vector3.z;
        }

        public static float Average(this Vector3 vector3)
        {
            return (vector3.x + vector3.y + vector3.z) / 3.0f;
        }

        public static float Average(this Vector2 vector2)
        {
            return (vector2.x + vector2.y) / 2.0f;
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
