using UnityEngine;
using UnityEngine.UI;

namespace DigitsNFCToolkit.Samples
{
	public class MessageScreenView: MonoBehaviour
	{
		private RectTransform writeMessageBox;
		private Text writeLabel;
		private Button writeCancelButton;
		private Button writeOKButton;

		private RectTransform pushMessageBox;
		private Text pushLabel;
		private Button pushCancelButton;
		private Button pushOKButton;

		private bool initialized;

		private void Initialize()
		{
			writeMessageBox = transform.Find("WriteMessageBox").GetComponent<RectTransform>();
			writeLabel = writeMessageBox.Find("Label").GetComponent<Text>();
			writeCancelButton = writeMessageBox.Find("CancelButton").GetComponent<Button>();
			writeOKButton = writeMessageBox.Find("OKButton").GetComponent<Button>();

			pushMessageBox = transform.Find("PushMessageBox").GetComponent<RectTransform>();
			pushLabel = pushMessageBox.Find("Label").GetComponent<Text>();
			pushCancelButton = pushMessageBox.Find("CancelButton").GetComponent<Button>();
			pushOKButton = pushMessageBox.Find("OKButton").GetComponent<Button>();
		}

		private void Awake()
		{
			if(!initialized) { Initialize(); }
		}

		public void Show()
		{
			gameObject.SetActive(true);
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}

		public void SwitchToPendingWrite()
		{
			if(!initialized) { Initialize(); }

			pushMessageBox.gameObject.SetActive(false);
			writeMessageBox.gameObject.SetActive(true);

			writeLabel.text = "Hold the nfc tag against your device to write the NDEF Message";
			writeCancelButton.gameObject.SetActive(true);
			writeOKButton.gameObject.SetActive(false);
		}

		public void SwitchToWriteResult(string writeResult)
		{
			if(!initialized) { Initialize(); }

			pushMessageBox.gameObject.SetActive(false);
			writeMessageBox.gameObject.SetActive(true);

			writeLabel.text = writeResult;
			writeCancelButton.gameObject.SetActive(false);
			writeOKButton.gameObject.SetActive(true);
		}

		public void SwitchToPendingPush()
		{
			if(!initialized) { Initialize(); }

			writeMessageBox.gameObject.SetActive(false);
			pushMessageBox.gameObject.SetActive(true);

			pushLabel.text = "Hold the other device against your device to push the NDEF Message";
			pushCancelButton.gameObject.SetActive(true);
			pushOKButton.gameObject.SetActive(false);
		}

		public void SwitchToPushResult(string pushResult)
		{
			if(!initialized) { Initialize(); }

			writeMessageBox.gameObject.SetActive(false);
			pushMessageBox.gameObject.SetActive(true);

			pushLabel.text = pushResult;
			pushCancelButton.gameObject.SetActive(false);
			pushOKButton.gameObject.SetActive(true);
		}
	}
}
