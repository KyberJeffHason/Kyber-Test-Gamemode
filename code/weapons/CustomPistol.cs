using Sandbox;

[Library( "weapon_custompistol", Title = "Custom Pistol", Spawnable = true )]
partial class CustomPistol : CustomWeapon
{
	public CustomPistol()
	{
		AmmoStock = 40;
		ClipSize = 25;
		Ammo = 10;
		EntityAmmoRestore = 10;
	}

	public override string ViewModelPath => "weapons/rust_pistol/v_rust_pistol.vmdl";

	public override float PrimaryRate => 15.0f;
	public override float SecondaryRate => 1.0f;

	public TimeSince TimeSinceDischarge { get; set; }

	public override void Spawn()
	{
		base.Spawn();

		SetModel( "weapons/rust_pistol/rust_pistol.vmdl" );

	}

	public override bool CanPrimaryAttack()
	{
		if ( Ammo > 0 )
		{
			return base.CanPrimaryAttack() && Input.Pressed( InputButton.Attack1 );
		}
		return false;
	}


	public override void AttackPrimary()
	{
		TimeSincePrimaryAttack = 0;
		TimeSinceSecondaryAttack = 0;

		(Owner as AnimEntity)?.SetAnimBool( "b_attack", true );

		ShootEffects();
		PlaySound( "rust_pistol.shoot" );
		ShootBullet( 0.05f, 1.5f, 9.0f, 3.0f );

		MinusAmmo();
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

	public override void OnReloadFinish()
	{
		IsReloading = false;

		int[] info = ReloadMoment( ClipSize, Ammo, AmmoStock );
		Ammo = info[0];
		AmmoStock = info[1];
	}

	protected override void OnPhysicsCollision( CollisionEventData eventData )
	{
		if ( eventData.Speed > 500.0f )
		{
			Discharge();
		}
	}
}
