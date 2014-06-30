using System.Collections.Generic;

public class PlayerCharacter : BaseCharacter {
	
	private static List<Item> _inventory = new List<Item>();
	public static List<Item> Inventory {
		get { return _inventory; }
	}
	
	void Update()
	{
		Messenger<int, int>.Broadcast("player health update", 80, 100, MessengerMode.DONT_REQUIRE_LISTENER);
	}
}
