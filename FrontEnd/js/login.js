document.getElementById("login-form").addEventListener("submit", async (e) => {
    e.preventDefault();

    const email = document.getElementById("email").value.trim();
    const password = document.getElementById("password").value.trim();
    const message = document.getElementById("message");

    // Simulação de login por enquanto
    if (email === "admin@teste.com" && password === "123456") {
        message.textContent = "Login bem-sucedido!";
        message.style.color = "green";

        // Simula salvar "userId" local e redireciona
        localStorage.setItem("userId", "admin");
        setTimeout(() => {
            window.location.href = "todo.html"; // futura tela de tarefas
        }, 1000);
    } else {
        message.textContent = "Email ou senha inválidos!";
        message.style.color = "red";
    }
});
