using System;
namespace TwinTechs {
	public interface IBack {
		event EventHandler AppBackPressed;
		void AppExit();
	}
}