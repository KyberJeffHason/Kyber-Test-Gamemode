using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox;

namespace sandbox.weapons
{
	[Library( "weapon_testpistol", Title = "PistolTest", Spawnable = true )]
	partial class PistolTest : Weapon
	{
		public override string ViewModelPath => "models/kyber/newpistol_test/main_v_custompistol.vmdl";

		public override float PrimaryRate => 15.0f;
		public override float SecondaryRate => 1.0f;

		public TimeSince TimeSinceDischarge { get; set; }

		public override void Spawn()
		{
			base.Spawn();

			SetModel( "models/kyber/newpistol_test/tp/actual/pistol_t.vmdl" );
		}

		public override bool CanPrimaryAttack()
		{
			return base.CanPrimaryAttack() && Input.Pressed( InputButton.Attack1 );
		}

		public override void AttackPrimary()
		{
			TimeSincePrimaryAttack = 0;
			TimeSinceSecondaryAttack = 0;

			(Owner as AnimEntity)?.SetAnimBool( "b_attack", true );

			ShootEffects();
			PlaySound( "rust_pistol.shoot" );
			ShootBullet( 0.05f, 1.5f, 9.0f, 3.0f );
		}

		private void Discharge()
		{
			if ( TimeSinceDischarge < 0.5f )
				return;

			TimeSinceDischarge = 0;

			var muzzle = GetAttachment( "muzzle" ) ?? default;
			var pos = muzzle.Position;
			var rot = muzzle.Rotation;

			ShootEffects();
			PlaySound( "rust_pistol.shoot" );
			ShootBullet( pos, rot.Forward, 0.05f, 1.5f, 9.0f, 3.0f );

			ApplyAbsoluteImpulse( rot.Backward * 200.0f );
		}

		protected override void OnPhysicsCollision( CollisionEventData eventData )
		{
			if ( eventData.Speed > 500.0f )
			{
				Discharge();
			}
		}
	}
}
