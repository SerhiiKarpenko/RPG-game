﻿using UnityEngine;

namespace CodeBase.Infrastructure.Asset_Management
{
	public interface IAssetProvider
	{
		GameObject Instantiate(string path);
		GameObject Instantiate(string path, Vector3 at);
	}
}