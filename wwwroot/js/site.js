// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/*Hodiny */
function updateClock() {
    const now = new Date();
    document.getElementById("clock").textContent =
        now.toLocaleTimeString('cs-CZ');
}

updateClock();
setInterval(updateClock, 1000);

/* Script pro filtrování */
	document.addEventListener('DOMContentLoaded', function () {
		if (sessionStorage.getItem("hideFiltr") === "true") {
			document.getElementById("formFiltr").style.display = "none";
		}
		else { document.getElementById("formFiltr").style.display = "flex"; }

		function nastavFiltr(checkboxId, selectId, buttonId) {
			const checkbox = document.getElementById(checkboxId);
			const select = document.getElementById(selectId);
			const button = document.getElementById(buttonId);
	


			if (select.value !== '') {
				checkbox.checked = true;
				select.style.display = 'inline-block';
				button.style.display = 'inline-block';
			}
			checkbox.addEventListener('change', function () {
				select.style.display = this.checked ? 'inline-block' : 'none';
				button.textContent = 'Filtrovat';
				button.style.display = this.checked ? 'inline-block' : 'none';
				if (!this.checked) {
					select.value = '';
				}
			});
		}

		nastavFiltr("chkPismeno", "selectPismeno", "btnPismeno");
		nastavFiltr("chkDruh", "selectDruh", "btnDruh");
	});

function hideFiltryTrue() {
	sessionStorage.setItem("hideFiltr", "true");
}
function hideFiltryFalse() {
	sessionStorage.setItem("hideFiltr", "false");
}