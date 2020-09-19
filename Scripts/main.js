(function () {
	"use strict";

	var treeviewMenu = $('.app-menu');

	// Toggle Sidebar
	$('[data-toggle="sidebar"]').click(function(event) {
		event.preventDefault();
		$('.app').toggleClass('sidenav-toggled');
	});

	// Activate sidebar treeview toggle
	$("[data-toggle='treeview']").click(function(event) {
		event.preventDefault();
		if(!$(this).parent().hasClass('is-expanded')) {
			treeviewMenu.find("[data-toggle='treeview']").parent().removeClass('is-expanded');
		}
		$(this).parent().toggleClass('is-expanded');
	});

	// Set initial active toggle
	$("[data-toggle='treeview.'].is-expanded").parent().toggleClass('is-expanded');

	//Activate bootstrip tooltips
    $("[data-toggle='tooltip']").tooltip();

    

    $(".datepicker").datepicker({
        changeMonth: true,
        changeYear: true,
        format: "dd/mm/yyyy",
        language: "tr",
    });
    $('.dataTable').DataTable({
       language: {
            url: '//cdn.datatables.net/plug-ins/1.10.19/i18n/Turkish.json'
     }
    });

    $(document).ready(function () {
        $(".select2").select2({
        });

        $('#durumModal').on('show.bs.modal', function (event) {
          //  debugger;
            var button = $(event.relatedTarget);
            var IhbarId = button.data("id");
            $("#employeeTable").DataTable().destroy();
            $("#employeeTable").DataTable(
                {
                    "ajax": {
                        "url": "../Ihbars/DurumListeleme/" + IhbarId,
                        "type": "Post",
                        "datatype": "json"
                    },
                    "columns": [
                        { "data": "IhbarId", "autoWidth": true },
                        { "data": "DurumTarih", "autoWidth": true },
                        { "data": "Aciklama", "autoWidth": true}

                    ]
                });
        });
    });

    var url = window.location.href;
    var anchor = $("a[href='" + url + "']");
    anchor.addClass("active");
    anchor.parent().parent().parent(".treeview").addClass("is-expanded");
    })();