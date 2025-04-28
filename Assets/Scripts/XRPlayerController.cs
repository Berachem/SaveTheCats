using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using System.Runtime.InteropServices;

public class XRPlayerController : MonoBehaviour
{

    public Transform headTransform;

    public InputActionReference leftGrabAction;
    public InputActionReference rightGrabAction;
    public InputActionReference talkAction;
    public TextMeshProUGUI indication;

    public XRRayInteractor rightRayInteractor; 
    public XRRayInteractor leftRayInteractor;

    private GameObject targetObject;
    private bool targetIsCat;
    private bool targetIsSpirit;
    private bool targetIsDeadCat;

    private GameObject grabbedCat;
    private GameObject grabbedDeadCat;

    private void Update()
    {
        UpdateTargetFromRay();

        if (grabbedCat == null && grabbedDeadCat == null)
        {
            if (targetIsCat)
            {
                CatController catController = targetObject.GetComponent<CatController>();
                if (!catController.isInSafeZone)
                {
                    indication.text = "Pressez les gachettes pour saisir le chat";
                    indication.gameObject.SetActive(true);

                    if (rightGrabAction.action.WasPressedThisFrame())
                    {
                        grabbedCat = targetObject;
                        catController.Meow();
                        grabbedCat.GetComponent<Rigidbody>().freezeRotation = true;
                        grabbedCat.GetComponent<Animator>().SetBool("Walk", false);
                        grabbedCat.GetComponent<Animator>().SetBool("Sit", true);
                        indication.gameObject.SetActive(false);
                    }
                }
                else
                {
                    indication.text = "Le chat est dans la SafeZone. Bravo !";
                    indication.gameObject.SetActive(true);
                }
            }
            else if (targetIsSpirit)
            {
                Talker talker = targetObject.GetComponent<Talker>();
                indication.text = "Pressez Bouton A pour discuter avec l'esprit";
                indication.gameObject.SetActive(true);

                if (talkAction.action.WasPressedThisFrame() && !talker.isInDialogue())
                {
                    talker.TriggerDialogue();
                }
            }
            else if (targetIsDeadCat)
            {
                DeadCatController deadCatController = targetObject.GetComponent<DeadCatController>();
                if (!deadCatController.isInSafeZone)
                {
                    indication.text = "Pressez les gachettes pour porter le corps du chat";
                    indication.gameObject.SetActive(true);

                    if (rightGrabAction.action.WasPressedThisFrame())
                    {
                        grabbedDeadCat = targetObject;
                        grabbedDeadCat.GetComponent<Rigidbody>().freezeRotation = true;
                        grabbedDeadCat.GetComponent<Rigidbody>().detectCollisions = false;
                        deadCatController.grabCat();
                        indication.gameObject.SetActive(false);
                    }
                }
                else
                {
                    indication.text = "Le chat est dans la SafeZone.";
                    indication.gameObject.SetActive(true);
                }
            }
            else
            {
                indication.gameObject.SetActive(false);
            }
        }
        else
        {
            bool isHolding = (rightGrabAction.action.IsPressed() || leftGrabAction.action.IsPressed());

            if (!isHolding)
            {
                if (grabbedCat)
                {
                    putDownCat();
                }
                else if (grabbedDeadCat)
                {
                    grabbedDeadCat.GetComponent<Rigidbody>().detectCollisions = true;
                    grabbedDeadCat.GetComponent<DeadCatController>().releaseCat();
                    grabbedDeadCat = null;
                }

                return;
            }

            if (grabbedCat)
            {
                Vector3 carryPosition = headTransform.position + headTransform.forward * 0.5f - new Vector3(0, 0.2f, 0);
                grabbedCat.transform.position = carryPosition;
                Vector3 lookDirection = new Vector3(headTransform.forward.x, 0, headTransform.forward.z);
                grabbedCat.transform.rotation = Quaternion.LookRotation(-lookDirection, Vector3.up);
            }
            else if (grabbedDeadCat)
            {
                Vector3 carryPosition = headTransform.position + headTransform.forward  - new Vector3(0, 0.3f, 0);
                grabbedDeadCat.transform.position = carryPosition;
                grabbedDeadCat.transform.rotation = headTransform.rotation * Quaternion.Euler(0, 90, 90);
            }

            if (rightGrabAction.action.WasPressedThisFrame())
            {
                if (grabbedCat)
                {
                    putDownCat();
                }
                else if (grabbedDeadCat)
                {
                    grabbedDeadCat.GetComponent<Rigidbody>().detectCollisions = true;
                    grabbedDeadCat.GetComponent<DeadCatController>().releaseCat();
                    grabbedDeadCat = null;
                }
            }
        }
    }

    private void UpdateTargetFromRay()
    {
        targetIsCat = false;
        targetIsSpirit = false;
        targetIsDeadCat = false;
        targetObject = null;

        RaycastHit hit;
        if (rightRayInteractor.TryGetCurrent3DRaycastHit(out hit) || leftRayInteractor.TryGetCurrent3DRaycastHit(out hit))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.CompareTag("Grabable"))
            {
                targetObject = hitObject;
                targetIsCat = true;
            }
            else if (hitObject.CompareTag("CatSpirit"))
            {
                targetObject = hitObject;
                targetIsSpirit = true;
            }
            else if (hitObject.CompareTag("DeadCat"))
            {
                targetObject = hitObject;
                targetIsDeadCat = true;
            }
        }
    }

    public void putDownCat()
    {
        grabbedCat.GetComponent<Animator>().SetBool("Walk", true);
        grabbedCat.GetComponent<Animator>().SetBool("Sit", false);
        grabbedCat = null;
    }
}
