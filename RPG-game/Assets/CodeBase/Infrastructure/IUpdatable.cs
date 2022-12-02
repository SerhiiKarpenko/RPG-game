using System;
using System.Collections;

namespace CodeBase.Infrastructure
{
	public interface IUpdatable
	{
		IEnumerator Update(Action action)
		{
			while (true)
			{
				action?.Invoke();
				yield return null;
			}	
		}
	}
}