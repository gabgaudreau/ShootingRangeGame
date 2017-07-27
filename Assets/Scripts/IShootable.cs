/**
File Created June 17th 2017 - File name = IShootable.cs
Author: Gabriel Gaudreau
Project: ShootingRangeGame
*/

public interface IShootable {
    /// <summary>
    /// Only function in this interface, executes when a target has been hit.
    /// </summary>
    /// <param name="objectHit">Object hit by a projectile</param>
    void GotShot(string objectHit);
}
