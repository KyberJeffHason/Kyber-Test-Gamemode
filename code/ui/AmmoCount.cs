using Sandbox.UI;
using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
	public class AmmoCount : Panel
	{
		public Label Label;

		public AmmoCount()
		{
			  Label = Add.Label( "0", "value" );
		}

		public override void Tick()
		{
			var player = Local.Pawn;
			SetClass( "active", Local.Pawn.ActiveChild is CustomWeapon w && w.Ammo >= 0);
			if ( player == null ) return;

			if ( Local.Pawn.ActiveChild is CustomWeapon f && f.Ammo >= 0 )
			{
				Label.Text = $"🗜{f.Ammo}/{f.AmmoStock}";
			}

	    }
	}
