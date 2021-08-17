using Sandbox;
using System;

[Library( "ent_ammo", Title = "AmmoEntity", Spawnable = true )]
public partial class Ammo : Prop, IUse
{
	public float MaxSpeed { get; set; } = 1000.0f;
	public float SpeedMul { get; set; } = 1.2f;

	public override void Spawn()
	{
		base.Spawn();

		SetModel( "models/kyber/ammobox/ammobox.vmdl" );
		//SetupPhysicsFromModel( PhysicsMotionType.Dynamic, false );
		//Scale = Rand.Float( 0.5f, 2.0f );
	}

	/*
	protected override void OnPhysicsCollision( CollisionEventData eventData )
	{
		var speed = eventData.PreVelocity.Length;
		var direction = Vector3.Reflect( eventData.PreVelocity.Normal, eventData.Normal.Normal ).Normal;
		Velocity = direction * MathF.Min( speed * SpeedMul, MaxSpeed );
	}
	*/
	public bool IsUsable( Entity user )
	{
		return true;
	}

	public bool OnUse( Entity user )
	{
		if ( user is Player player )
		{
			/*
			if ( player.ActiveChild is CustomWeapon f )
			{
				Event.Run( "kes.ammopickup" );
			}
			*/

			for ( int i = 0; i < 9; i++ )
			{
				var check = player.Inventory.GetSlot( i );
				//Log.Info( $"{check}" );

				if ( check is CustomWeapon d)
				{
					//Log.Info( $"FOUND!!!" );
					Event.Run( "kes.ammopickup" );
				}
			}

			Delete();
		}

		return false;
	}
}
