# Furrusion Prefab Tools

**Unity Editor tools for inspecting prefab variant inheritance chains.**

This package provides two utilities:
- 🧩 **Prefab Inspector** – View the full inheritance chain of a single prefab
- 📦 **Batch Inspection** – Export inheritance chains of all prefabs in your project

---

## 📦 Installation

### 🛠 ALCOM (Automatic Lightweight Community Open Manager)

If you are using [ALCOM](https://alcom.app):

1. Open your Unity project
2. Go to **ALCOM** → **Package Manager**
3. Click **Install from Git**
4. Enter:
   ```
   https://github.com/furrusion/furrusion-prefab-tools.git
   ```
5. Click **Install** and refresh

---

### 🧪 Creator Companion (VRChat)

If you are using VRChat Creator Companion (VCC):

1. Open your project in VCC
2. Go to **Manage Packages**
3. Click **Add Git URL**
4. Paste:
   ```
   https://github.com/furrusion/furrusion-prefab-tools.git
   ```
5. Confirm and install

---

### 🔧 Unity Package Manager (UPM)

If you are using Unity Package Manager directly:

Add this to your `manifest.json`:

```json
"org.furrusion.prefabtools": "https://github.com/furrusion/furrusion-prefab-tools.git"
```

Or use Unity’s **Package Manager → Add package from Git URL...**:

```
https://github.com/furrusion/furrusion-prefab-tools.git
```

---

## 🧭 Menu Location

You’ll find the tools in:

```
Furrusion → Tools → Prefab Variant Chain
├── Prefab Inspector
└── Batch Inspection
```

---

## ✅ Features

- View parent → base prefab chains using Unity's internal prefab linkage system
- Supports saving output to text files (`Assets/PrefabChains/*.txt`)
- Compatible with both unpacked and nested variant prefabs
- Does **not** modify your assets

---

## 🧰 Requirements

- Unity **2022.3** or newer  
  (Tested with **2022.3.22f1**)

---

## 📄 License

This package is released under the [MIT License](LICENSE).

---

## 🐾 Created by [Furrusion.org](https://furrusion.org)

Building tools for creators in VR, Unity, and virtual production.
