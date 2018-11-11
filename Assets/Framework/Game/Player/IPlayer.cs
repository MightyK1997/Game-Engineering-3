using UnityEngine;

public interface IPlayer {

	Color Color { get; }
	string Name { get; }
    float Gold { get; set; }
    bool IsLocal { get; }

}

