using System;
using System.Linq;
using easyInputs;
using Photon.Pun;
using UnityEngine;
using UnhollowerBaseLib;
using UnityEngine.UI;

namespace MenuTemplate.Menu
{
    #region MADEBYZINX Edited By LMB Yeetus
	//dont delete this if you do you skidding and thats annoying
	//also dont mess up anything
    #endregion
    public class Menu
	{
        private static bool[] PageButton = new bool[5];
        private static bool[] Page1inrange = new bool[19];
        public static bool[] Page1ButtonActive = new bool[19];
		#region Bools
        private static GameObject menu = null;
		private static GameObject canvasObj = null;
		private static GameObject reference = null;
		public static string Page = "1";
		public static Material MenuTheme = new Material(Shader.Find("Sprites/Default"));
		public static string MenuColorType = "normal";
		private static float offset;
		private static float lastClickTime = 0.0f;
		public static float cooldownDuration = 0.25f;
		private static float elapsedTime = 0f;
		private static Color ButtonClicked;
		private static Color ButtonNotClicked;
		private static Color startColor = new Color(0.5f, 0, 0.5f);
        private static Color endColor = new Color(0.2f, 0.2f, 0.2f);
        private static float transitionDuration = 3f;
		public static float proximityRange = 0.037f;
		#endregion
		#region Menu
		public static void Draw()
		{
			menu = GameObject.CreatePrimitive(PrimitiveType.Cube);
			UnityEngine.Object.Destroy(menu.GetComponent<Rigidbody>());
			UnityEngine.Object.Destroy(menu.GetComponent<BoxCollider>());
			UnityEngine.Object.Destroy(menu.GetComponent<Renderer>());
			menu.transform.localScale = new Vector3(0.1f, 0.29f, 0.4f);
			GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
			UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
			UnityEngine.Object.Destroy(gameObject.GetComponent<BoxCollider>());
			gameObject.transform.parent = menu.transform;
			gameObject.transform.rotation = Quaternion.identity;
			gameObject.transform.localScale = new Vector3(0.1f, 1.05f, 0.9f);
			gameObject.GetComponent<Renderer>().material = MenuTheme;
			gameObject.transform.position = new Vector3(0.05f, 0f, 0f);
			canvasObj = new GameObject();
			canvasObj.transform.parent = menu.transform;
			Canvas canvas = canvasObj.AddComponent<Canvas>();
			CanvasScaler canvasScaler = canvasObj.AddComponent<CanvasScaler>();
			canvasObj.AddComponent<GraphicRaycaster>();
			canvas.renderMode = RenderMode.WorldSpace;
			canvasScaler.dynamicPixelsPerUnit = 1000f;
			GameObject gameObject2 = new GameObject();
			gameObject2.transform.parent = canvasObj.transform;
			Text text = gameObject2.AddComponent<Text>();
			text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
			text.text = " LMB Holdable Menu <color=cyan>[V1]</color> [" + Page + "] ";
			text.color = Color.white;
			text.fontSize = 1;
			text.alignment = TextAnchor.MiddleCenter;
			text.resizeTextForBestFit = true;
			text.resizeTextMinSize = 0;
			RectTransform component = text.GetComponent<RectTransform>();
			component.localPosition = Vector3.zero;
			component.sizeDelta = new Vector2(0.28f, 0.05f);
			component.position = new Vector3(0.06f, 0f, 0.145f);
			component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
		}
		#endregion
		#region Buttons
		public static void DrawPageButton(string Text, float textypos, float buttonypos, string Pagenum, ref bool Button, int index)
		{
			GameObject DisButton = GameObject.CreatePrimitive(PrimitiveType.Cube);
			UnityEngine.Object.Destroy(DisButton.GetComponent<Rigidbody>());
			UnityEngine.Object.Destroy(DisButton.GetComponent<BoxCollider>());
			DisButton.transform.parent = menu.transform;DisButton.transform.rotation = menu.transform.rotation;
			DisButton.transform.localScale = new Vector3(0.05f, 0.8f, 0.08f);
			DisButton.transform.localPosition = new Vector3(0.54f, 0f, buttonypos - offset / 1.2f);
			Text text2 = new GameObject
			{transform ={parent = canvasObj.transform}}.AddComponent<Text>();
			text2.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
			text2.text = Text;
			text2.color = new Color(255, 255, 255);
            text2.fontSize = 1;
			text2.alignment = TextAnchor.MiddleCenter;
			text2.resizeTextForBestFit = true;
			text2.resizeTextMinSize = 0;
			RectTransform component = text2.GetComponent<RectTransform>();
			component.localPosition = Vector3.zero;
			component.sizeDelta = new Vector2(0.2f, 0.03f);
			component.localPosition = new Vector3(0.064f, 0f, textypos - offset / 3.05f);
			Quaternion newRotation = DisButton.transform.rotation * Quaternion.Euler(180f, 90f, 90f);
			component.rotation = newRotation;
			float isclicked = Vector3.Distance(DisButton.transform.position, reference.transform.position); 
			if (isclicked <= proximityRange && Time.time - lastClickTime > cooldownDuration)
			{
				Button = true;
				PageButton[index] = Button;
				Page = Pagenum;
				GorillaTagger.Instance.offlineVRRig.PlayHandTap(14, false, 0.05f);
				GameObject.Destroy(menu);
				GameObject.Destroy(reference);
				menu = null; reference = null;
				lastClickTime = Time.time;
			}
			else if (isclicked > proximityRange)
			{
				Button = false;
				PageButton[index] = Button;
			} 
			if (Button)
			{ 
				DisButton.GetComponent<Renderer>().material.color = ButtonClicked;
			} 
			else 
			{ 
				DisButton.GetComponent<Renderer>().material.color = ButtonNotClicked;
            }
			GameObject.Destroy(DisButton, Time.deltaTime);
			GameObject.Destroy(text2, Time.deltaTime);
		}
		public static void Drawsingleclickbutton(string Text, float textypos, float buttonypos, ref bool ButtonRange, ref bool Mod, int index)
		{
			GameObject DisButton = GameObject.CreatePrimitive(PrimitiveType.Cube);
			UnityEngine.Object.Destroy(DisButton.GetComponent<Rigidbody>()); 
			UnityEngine.Object.Destroy(DisButton.GetComponent<BoxCollider>()); 
			DisButton.transform.parent = menu.transform;
			DisButton.transform.rotation = menu.transform.rotation;
			DisButton.transform.localScale = new Vector3(0.05f, 0.8f, 0.08f); 
			DisButton.transform.localPosition = new Vector3(0.54f, 0f, buttonypos - offset / 1.2f);
			DisButton.GetComponent<Renderer>().material.color = Color.black;
			Text text2 = new GameObject { transform = { parent = canvasObj.transform } }.AddComponent<Text>();
			text2.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
			text2.text = Text; text2.color = new Color(255, 255, 255);
            text2.fontSize = 1;
			text2.alignment = TextAnchor.MiddleCenter;
			text2.resizeTextForBestFit = true;
			text2.resizeTextMinSize = 0;
			RectTransform component = text2.GetComponent<RectTransform>();
			component.localPosition = Vector3.zero;
			component.sizeDelta = new Vector2(0.2f, 0.03f);
			component.localPosition = new Vector3(0.064f, 0f, textypos - offset / 3.05f);
			Quaternion newRotation = DisButton.transform.rotation * Quaternion.Euler(180f, 90f, 90f); 
			component.rotation = newRotation; 
			float isclicked = Vector3.Distance(DisButton.transform.position, reference.transform.position);
			if (isclicked <= proximityRange && !ButtonRange && Time.time - lastClickTime > cooldownDuration) 
			{
				ButtonRange = true; Mod = !Mod;
				GorillaTagger.Instance.offlineVRRig.PlayHandTap(14, false, 0.05f); 
				lastClickTime = Time.time; } else if (isclicked > proximityRange && ButtonRange)
			{
				ButtonRange = false; 
			} 
			if (Mod) 
			{
                text2.color = new Color(0.5f, 0, 0.5f);
            }
			else 
			{ 
                text2.color = new Color(255, 255, 255);
            }
			Page1inrange[index] = ButtonRange; 
			Page1ButtonActive[index] = Mod;
			GameObject.Destroy(DisButton, Time.deltaTime);
			GameObject.Destroy(text2, Time.deltaTime);
		}
		#endregion
		#region dont using
		public static void Prefix()
		{
			if (EasyInputs.GetSecondaryButtonDown(EasyHand.LeftHand) && menu == null)
			{
				Draw();
				if (reference == null)
				{
					reference = GameObject.CreatePrimitive(PrimitiveType.Sphere);
					GameObject.Destroy(reference.GetComponent<SphereCollider>());
					reference.transform.parent = GorillaLocomotion.Player.Instance.rightHandTransform;
					reference.transform.localPosition = new Vector3(0f, 0f, 0.1f);
					reference.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
				}
			}
			else
			{
				if (!EasyInputs.GetSecondaryButtonDown(EasyHand.LeftHand) && menu != null)
				{
					Rigidbody menuRigidbody = menu.AddComponent<Rigidbody>();

					Rigidbody referenceRigidbody = reference.AddComponent<Rigidbody>();

					menuRigidbody.useGravity = false;
					menuRigidbody.mass = 0.06f;
					referenceRigidbody.mass = 0.06f;

					menuRigidbody.velocity = new Vector3(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1));
					menuRigidbody.angularVelocity = new Vector3(UnityEngine.Random.Range(-33, 33), UnityEngine.Random.Range(-33, 33), UnityEngine.Random.Range(-33, 33));

					GameObject.Destroy(menu, 3);
					GameObject.Destroy(reference, 3);

					menu = null;
					reference = null;
				}
			}
			if (EasyInputs.GetSecondaryButtonDown(EasyHand.LeftHand) && menu != null)
			{
				menu.transform.position = GorillaLocomotion.Player.Instance.leftHandTransform.position;
				menu.transform.rotation = GorillaLocomotion.Player.Instance.leftHandTransform.rotation;
			}
			#endregion

			if (EasyInputs.GetSecondaryButtonDown(EasyHand.LeftHand) && menu != null)
			{

				if (Page == "1")
				{
					DrawPageButton("Page 1", 0.077f, 0.19f, "2", ref PageButton[2], 2);
					DrawPageButton("Page 2", 0.037f, 0.09f, "3", ref PageButton[3], 3);
					DrawPageButton("Page 3", 0.197f, 0.49f, "4", ref PageButton[4], 4);
				}
				if (Page == "2")//This is Page 1
				{
					Drawsingleclickbutton("Disconnect", 0.077f, 0.19f, ref Page1inrange[1], ref Page1ButtonActive[1], 1);
					Drawsingleclickbutton("Soon <color=lime>[W]</color>", 0.037f, 0.09f, ref Page1inrange[2], ref Page1ButtonActive[2], 2);
					Drawsingleclickbutton("Soon <color=lime>[W]</color>", 0.197f, 0.49f, ref Page1inrange[3], ref Page1ButtonActive[3], 3);
					Drawsingleclickbutton("Soon <color=lime>[W]</color>", -0.003f, -0.01f, ref Page1inrange[4], ref Page1ButtonActive[4], 4);
					Drawsingleclickbutton("Soon <color=lime>[W]</color>", -0.043f, -0.11f, ref Page1inrange[5], ref Page1ButtonActive[5], 5);
					Drawsingleclickbutton("Soon <color=lime>[W]</color>", -0.083f, -0.21f, ref Page1inrange[6], ref Page1ButtonActive[6], 6);
                    DrawPageButton("Back", -0.123f, -0.31f, "1", ref PageButton[1], 1);
                }
				if (Page == "3")//This is Page 2
				{
					Drawsingleclickbutton("Disconnect", 0.077f, 0.19f, ref Page1inrange[1], ref Page1ButtonActive[1], 1);
					Drawsingleclickbutton("Soon <color=lime>[W]</color>", 0.037f, 0.09f, ref Page1inrange[7], ref Page1ButtonActive[7], 7);
					Drawsingleclickbutton("Soon <color=lime>[W]</color>", 0.197f, 0.49f, ref Page1inrange[8], ref Page1ButtonActive[8], 8);
					Drawsingleclickbutton("Soon <color=lime>[W]</color>", -0.003f, -0.01f, ref Page1inrange[9], ref Page1ButtonActive[9], 9);
					Drawsingleclickbutton("Soon <color=lime>[W]</color>", -0.043f, -0.11f, ref Page1inrange[10], ref Page1ButtonActive[10], 10);
					Drawsingleclickbutton("Soon <color=lime>[W]</color>", -0.083f, -0.21f, ref Page1inrange[11], ref Page1ButtonActive[11], 11);
                    DrawPageButton("Back", -0.123f, -0.31f, "1", ref PageButton[1], 1)
                }
				if (Page == "4")//This is Page 3
				{
					Drawsingleclickbutton("Disconnect", 0.077f, 0.19f, ref Page1inrange[1], ref Page1ButtonActive[1], 1);
					Drawsingleclickbutton("Soon <color=lime>[W]</color>", 0.037f, 0.09f, ref Page1inrange[12], ref Page1ButtonActive[12], 12);
					Drawsingleclickbutton("Soon <color=lime>[W]</color>", 0.197f, 0.49f, ref Page1inrange[13], ref Page1ButtonActive[13], 13);
					Drawsingleclickbutton("Soon <color=lime>[W]</color>", -0.003f, -0.01f, ref Page1inrange[14], ref Page1ButtonActive[14], 14);
					Drawsingleclickbutton("Soon <color=lime>[W]</color>", -0.043f, -0.11f, ref Page1inrange[15], ref Page1ButtonActive[15], 15);
					Drawsingleclickbutton("Soon <color=lime>[W]</color>", -0.083f, -0.21f, ref Page1inrange[16], ref Page1ButtonActive[16], 16);
                    DrawPageButton("Back", -0.123f, -0.31f, "1", ref PageButton[1], 1);
                }
			}
			#region TheButtonActivationThings

			// page 1
			if (Page1ButtonActive[1])
			{
				PhotonNetwork.Disconnect();
				Page1ButtonActive[1] = false;
			}
			if (Page1ButtonActive[2])
			{

			}
			if (Page1ButtonActive[3])
			{

			}
			if (Page1ButtonActive[4])
			{

			}
			if (Page1ButtonActive[5])
			{

			}
			if (Page1ButtonActive[6])
			{

			}

			// page 2
			if (Page1ButtonActive[1])
			{
				PhotonNetwork.Disconnect();
				Page1ButtonActive[1] = false;
			}
			if (Page1ButtonActive[7])
			{

			}
			if (Page1ButtonActive[8])
			{

			}
			if (Page1ButtonActive[9])
			{

			}
			if (Page1ButtonActive[10])
			{

			}
			if (Page1ButtonActive[11])
			{

			}

			// page 3
			if (Page1ButtonActive[1])
			{
				PhotonNetwork.Disconnect();
				Page1ButtonActive[1] = false;
			}
			if (Page1ButtonActive[12])
			{

			}
			if (Page1ButtonActive[13])
			{

			}
			if (Page1ButtonActive[14])
			{

			}
			if (Page1ButtonActive[15])
			{

			}
            if (Page1ButtonActive[16])
            {

            }


            #endregion

            #region MenuColorChanger
            if (MenuColorType == "normal")
			{
				ButtonClicked = Color.grey;
				ButtonNotClicked = new Color(0, 0, 0);

				if (elapsedTime < transitionDuration)
				{
					MenuTheme.color = Color.Lerp(startColor, endColor, elapsedTime / transitionDuration);
					elapsedTime += Time.deltaTime;
				}
				else if (elapsedTime >= transitionDuration && MenuTheme.color != startColor)
				{
					MenuTheme.color = Color.Lerp(endColor, startColor, (elapsedTime - transitionDuration) / transitionDuration);
					elapsedTime += Time.deltaTime;
				}
				else if (elapsedTime >= transitionDuration && MenuTheme.color == startColor)
				{
					elapsedTime = 0f;
				}
			}
		}
            #endregion

            #region Mods
			public class PutYourModsHere
			{

			}
			#endregion
	}
}
