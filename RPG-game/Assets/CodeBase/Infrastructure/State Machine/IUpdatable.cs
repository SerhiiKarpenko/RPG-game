﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace CodeBase.Infrastructure.State_Machine
{
	public interface IUpdatable
	{
		IEnumerator Update(List<Action> actions)
		{
			while (true)
			{
				foreach (Action action in actions)
					action?.Invoke();
				
				yield return null;
			}	
		}
	}
}