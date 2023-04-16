let createCrud = function (crudParam) {
    let controllerName = crudParam.controllerName;
    let crudId = crudParam.crudId;
    let gridId = crudParam.gridId;
    let url = {
        create: controllerName + '/Create',
        edit: controllerName + '/Edit',
        detail: controllerName + '/Detail',
        delete: controllerName + '/Delete',
        gridList: controllerName + '/GridList',
        exportToExcel: controllerName + '/ExportToExcel',
        getAdaptiveFilter: controllerName + '/GetAdaptiveFilter',

        createChild: controllerName + '/CreateChild',
    };

    // initiate grid
    initGrid(gridId, controllerName, crudParam.gridConfig.size, crudParam.gridConfig.orderBy);

    // listener form submit setelah form di append di modal
    function initForm() {

        // SUBMIT FORM CREATE / UPDATE
        $("#ModalForm form").submit(function (e) {

            e.preventDefault(); // avoid to execute the actual submit of the form.

            var form = $(this);
            var actionUrl = form.attr('action');
            var formState = $("#ModalForm form").attr("form-state");
            setModalLoading(true);

            var formSerialize = form.serialize();


            $.ajax({
                type: "POST",
                url: actionUrl,
                data: formSerialize,
                success: function (partialView) {
                    setModalLoading(false);
                    if (partialView) {
                        $("#ModalForm .append-here").html($(partialView));
                        initForm();
                    } else {
                        var successMessage = "Data has been added";
                        if (formState == "Edit") {
                            successMessage = "Data has been updated"
                        }

                        showToast(successMessage);
                        $("#ModalForm").modal('hide');
                        adsGrid[gridId].reload();
                    }
                },
                error: function (data) {
                    setModalLoading(false);
                    alert("An Error has occured...");
                    showToastError('An Error has occured...');
                }

            });

        });
    }

    // clear modal form ketika mau di show dengan tampilan loading spinner
    function clearForm(id) {
        var loadingOverlay = '<div class="modal-content"><div class="modal-body text-center"><div class="spinner-border my-5"></div></div></div>';
        $(id + " .append-here").html(loadingOverlay);
    }

    // show modal sambil nunggu form dari server
    function showCreateForm() {
        clearForm("#ModalForm");
        $("#ModalForm").modal('show');
        $.get(url.create, function (partialView) {

            $("#ModalForm .append-here").html($(partialView));


            initForm();
        })
    }

    // show modal sambil nunggu form render dari server
    function showEditForm(id) {
        clearForm("#ModalForm");
        $("#ModalForm").modal('show');
        $.get(url.edit + "/" + id, function (partialView) {

            $("#ModalForm .append-here").html($(partialView));
            initForm();
        })
    }

    // show modal sambil nunggu form render dari server
    function showReadonlyForm(id) {
        clearForm("#ModalForm");
        $("#ModalForm").modal('show');
        $.get(url.detail + "/" + id, function (partialView) {
            $("#ModalForm .append-here").html($(partialView));
        })
    }



    // show modal confirm delete
    let selectedId = null;
    function showConfirmDelete(id, name) {
        $("#ModalDelete").modal('show');
        $("#ModalDelete .delete-text-name").text(name);
        selectedId = id;
    }

    // set modal loading untuk form / delete
    let submitBtnTextTemp = '';
    function setModalLoading(isShow) {
        if (isShow) {

            $(".modal.show button").prop("disabled", true);
            submitBtnTextTemp = $(".modal.show button[type='submit']").text();
            $(".modal.show button[type='submit']").html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>');
        } else {

            $(".modal.show button").prop("disabled", false);
            $(".modal.show button[type='submit']").html(submitBtnTextTemp);
        }
    }

    // confirm delete ke server
    function confirmDelete() {
        setModalLoading(true);

        $.post(url.delete + "/" + selectedId, function (response) {
            setModalLoading(false);
            selectedId = null;

            $("#ModalDelete").modal('hide');
            adsGrid[gridId].reload();
            showToast('Item has been deleted');
        }).fail(function () {
            setModalLoading(false);
            alert("An Error has occured...");
            showToastError('An Error has occured...');
        });
    }


    // listener form submit setelah child form di append di modal
    function initChildForm() {

        // SUBMIT FORM CREATE / UPDATE
        $("form.modal-child-form").submit(function (e) {

            e.preventDefault(); // avoid to execute the actual submit of the form.

            var form = $(this);
            var actionUrl = form.attr('action');
            var formState = form.attr("form-state");
            var formSerialize = form.serialize();

            var listChild = $("#" + currentChildFormId + " input").serialize();
            listChild = listChild.replaceAll(currentChildFormId, "");

            var fieldIdParam = "&fieldId=" + currentChildFormId;
            $.ajax({
                type: "POST",
                url: actionUrl,
                data: formSerialize + "&" + listChild + fieldIdParam,
                success: function (partialView) {
                    /// setModalLoading(false);
                    if (partialView.indexOf("modal-child-form") < 0) {
                        $("#" + currentChildFormId).find('.append-child-grid-here').html($(partialView));
                    } else {
                        $(".modal-child-form").remove();

                    }

                    hideChildForm();
                },
                error: function (data) {
                    setModalLoading(false);
                    alert("An Error has occured...");
                    showToastError('An Error has occured...');
                }

            });

        });
    }

    let currentChildFormId = null;
    // show modal sambil nunggu form dari server
    function showChildCreateForm(fieldTypeId) {

        currentChildFormId = fieldTypeId;
        clearForm(".modal-child-form");

        // main modal to left
        $("#ModalForm .modal-content").eq(0).addClass("modal-toleft");
        $(".modal-block-overlay").show();

        // new modal active
        $.get(url.createChild + "/" + fieldTypeId, function (partialView) {
            $("#ModalForm .append-here").append($(partialView));
            initChildForm();
            setTimeout(function () {
                $(".modal-child-form").addClass('show');
            }, 5);

        })
    }


    function hideChildForm() {
        $("#ModalForm .modal-content").eq(0).removeClass("modal-toleft");
        $(".modal-block-overlay").hide();
        $(".modal-child-form.show").removeClass('show');

        setTimeout(function () {
            $(".modal-child-form").remove();
        }, 300);



    }

    function deleteChildRow(e) {
        let rowToBeDelete = $(e).closest('tr');
        rowToBeDelete.remove();
    }

    function toggleModalSize(e) {
        $("#ModalForm .modal-dialog").toggleClass("modal-xl");
        
      
        $(e).find("i").toggleClass("fa-expand");
        $(e).find("i").toggleClass("fa-compress");
    }

    return {
        initForm,

        showCreateForm,
        showEditForm,
        showReadonlyForm,
        grid: adsGrid[gridId],
        showConfirmDelete,
        confirmDelete,

        showChildCreateForm,
        deleteChildRow,
        hideChildForm,
        toggleModalSize
    }
}
