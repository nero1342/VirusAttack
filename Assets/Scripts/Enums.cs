using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
public enum SlingshotState
{
        Idle,
        UserPulling,
        BirdFlying
}

public enum GameState
{
        Start,
        Playing,
        Won,
        Lost
}


public enum GunState
{
        Inactive,
        Idle,
        BeforeThrown,
        Thrown
}

}
