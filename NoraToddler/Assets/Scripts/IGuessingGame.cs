/*
This file is part of Nora. 

Nora is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Nora is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with Nora. If not, see <http://www.gnu.org/licenses/>.
*/
using UnityEngine;
public interface IGuessingGame
{
    void OnIncorrect(int CurrentCorrectIndex);
    void PlayInstruction(int CurrentCorrectIndex);
    ParticleSystem GetParticle(int i);
    void PlayAffirmationAudio(int CorrectIndex);
    void SetSpriteData(int CurrentNumSprites);
    void OnClickGuess(int index);
    void PlayInstruction();
}