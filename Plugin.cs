using System;
using MenuTemplate.Menu;
using MelonLoader;
using Photon.Pun;
using GorillaNetworking;
using UnityEngine;

namespace MenuTemplate
{
	public class Plugin : MelonMod
	{
        #region Main

        public bool menuInitialized;

		public override void OnUpdate()
		{
			if (!this.menuInitialized)
			{
				this.menuInitialized = true;
				//if you want a mod to be enabled on load put this
				//Menu.Menu.Page1ButtonActive[1] = true;
			}

			Menu.Menu.Prefix();
			if (!PhotonNetwork.IsConnected)
			{
				PhotonNetwork.ConnectUsingSettings();
			}
		}

		#endregion

	}
}
