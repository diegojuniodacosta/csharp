
[HttpPost]
public async Task<IActionResult> Login([FromRoute] string username, string password)
{
	bool resultado = _loginService.Login(username, password);
	return Ok(resultado);
}
 
[HttpGet]
public async Task<IActionResult> GetAllUsuarios()
{
	List<Usuario> usuariosFromDatabase = await _loginService.GetAllUsuariosAsync();
	return Ok(usuariosFromDatabase);
}
 
[HttpGet("/{usuarioId:int}")]
public async Task<IActionResult> GetUsuario([FromRoute] int usuarioId)
{
	if (usuarioId is null)
	{
		return BadRequest("O ID do usuário não foi informado.");
	}
	Usuario usuario = await _loginService.GetUsuarioAsync(usuarioId);
	return Ok(usuario);
}
 
public class LoginService
{
	public bool Login(string username, string password)
	{
		Usuario usuario = _context.Usuarios.Where(x => x.Username == username && x.Password == password);
		if (usuario is null)
		{
			throw new Exception("Credenciais inválidas.");
		}
		return true;
	}
	public bool GetUsuario(ind usuarioId)
	{
		Usuario usuario = _context.Usuarios.FirstOrDefaultAsync(x => x.Id == usuarioId);
		if (usuario is null)
		{
			throw new Exception("Usuário não encontrado.");
		}
		return usuario;
	}
}